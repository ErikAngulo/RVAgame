using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using System.IO;
using System.Globalization;
using System.Linq;

public class gameController : MonoBehaviour
{

    // gameController script controls main functioalities of shooting game:
    // 1. initialize the game
    // 2. control what happens after correclty hit a target
    // 3. finish the game and calculate scores

    // Write game statistics at user csv
    public IOController ioController;

    // Game canvas (show current points and remaining play time)
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _latestHitText;
    private TextMeshProUGUI _remainingTime;
    
    // Light Initialization
    public Light blueLight;
    public Light orangeLight;

    // Dartboard movement configuration
    public bool movement;
    // Sound to play when light turns on
    public AudioSource soundLight;
    // Save which light to turn on next
    private Light _choosedLight;
    // Play time configuration
    public float playTime;
    // Remaining play time
    private float _totalTime;
    // Total points
    private float _score = 0;
     
    // List to choose random light for initilization
    private List<Light> _optionLight = new List<Light>();
    // Watining time to turn on a light again
    private float wait = -1.0f;
    // Save when a collision happened
    private bool _collision = false;
    // Scene to go once game finishes
    private string _scoreScene = "GameOverScene";

    // Gun controller to get used darts
    private gunController guncontroller;
    // Game finished
    private bool _finished = false;

    // Time needed to hit the correct target since its light turned on
    private float _timeNeededToHit = 0.0f;

    // Game information to calculate statistics
    // n correct target hit --> n entries

    // Light turned on (n entries) 
    private List<string> _nLight = new List<string>();
    // Time needed to hit correct target (n entries)
    private List<float> _nTimeToHit = new List<float>();
    // Collision location at X axis (n entries)
    private List<float> _nCoordX = new List<float>();
    // Collision location at Y axis (n entries)
    private List<float> _nCoordY = new List<float>();
    // Scored points (n entries)
    private List<float> _nPoints = new List<float>();
    // Bullets used (n entries)
    private List<int> _nBulletsToHit = new List<int>();
    
    // Start is called before the first frame update
    // Here the game is initialized
    void Start()
    {
        _totalTime = playTime;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Load UI elements to show user info
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _latestHitText = GameObject.Find("LatestHitText").GetComponent<TextMeshProUGUI>();
        _remainingTime = GameObject.Find("RemainingTimeText").GetComponent<TextMeshProUGUI>();

        // The gun is by default as a right controller (oculus quest 2 controller)
        // If user selected to play with left controller, associate the gun to left controller
        if (StaticClass.Controller.Equals("Left")){ //move from right to left controller
          GameObject gun = GameObject.Find("Gun").gameObject;
          GameObject _leftController = GameObject.Find("LeftHandAnchor").gameObject;
          gun.transform.SetParent(_leftController.transform, true);
        }

        // Save both lights, choose one randomly, and turn it on
        _optionLight.Add(blueLight);
        _optionLight.Add(orangeLight);
        int index = UnityEngine.Random.Range(0, _optionLight.Count);
        _choosedLight = _optionLight[index];
        _choosedLight.enabled = true;

        // Tell dartboardController scripts if targets should be moved (according to user preference)
        dartboardController scriptOrange = GameObject.Find("orange").GetComponent<dartboardController>();
        scriptOrange.movement = movement;
        dartboardController scriptBlue = GameObject.Find("blue").GetComponent<dartboardController>();
        scriptBlue.movement = movement;

        guncontroller = GameObject.Find("Gun").GetComponent<gunController>();
    }

    // Update is called once per frame
    void Update()
    {  
      wait -= Time.deltaTime;
      // Once the dart collisioned at correct target, its light turned off
      // After a period of time, a light chosen randomly is turned on
      if (wait <= 0 && _collision){
            int index = UnityEngine.Random.Range(0, _optionLight.Count);
            _choosedLight = _optionLight[index];
            _choosedLight.enabled = true;
            _collision = false;
            soundLight.Play();
            
            _timeNeededToHit = 0.0f;
      }
      playTime -= Time.deltaTime;
      // Update remaining time at game canvas
      _remainingTime.text = "Time: " + playTime.ToString("F1") + "s";
      // Once game time is up, write game statistic to user csv and show the user game summary results
      if (playTime < 0.0f && !_finished){
        _finished = true;
        // Calculate statistics from play and write it to user csv
        ioController.WriteStatistics2(movement, _totalTime, _nLight, _nTimeToHit, _nCoordX, _nCoordY, _nPoints, _nBulletsToHit);
        // Calculate summary play info and save it to show at score scene
        getScores();
        GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(_scoreScene);
      }
      // Increment time needed to hit until target hit
      _timeNeededToHit += Time.deltaTime;            
    }

    // Script called when detecting a correct target collision from LightSwitch
    // Which light, scored points and the coordinates are received in order to save info
    // Management of turning off light and adapting parameters to next turned on light
    public void TargetHit(Light lightTargetTouched, float _points, Vector3 _coord){
        //coordX and coordY considering centre of target is 0,0

        // Show user current hit points and total score
        _score += _points;
        _scoreText.text = "Score: " + _score.ToString("F2");
        _latestHitText.text = "Latest: " + _points.ToString("F2");

      // Turn off hit dartboard light
       lightTargetTouched.enabled = false;
       _collision = true;
       // Randomly choose a time period which will be waited until a random light will turn on
       wait = UnityEngine.Random.Range(0.5f,5.0f);

      // save instance each hit
      if (lightTargetTouched.GetInstanceID() == blueLight.GetInstanceID()){
        _nLight.Add("Blue");
      }
      else if (lightTargetTouched.GetInstanceID() == orangeLight.GetInstanceID()){
        _nLight.Add("Orange");
      }
      else{
        _nLight.Add("Unknown");
      }
      // Save more info, like time needed to hit, coordinates, points, how many darts were fired to hit
      _nTimeToHit.Add(_timeNeededToHit);
      _nCoordX.Add(_coord.x);
      _nCoordY.Add(_coord.y);
      _nPoints.Add(_points);
      int bulletsUsed = guncontroller.dartsFiredAndReset();
      _nBulletsToHit.Add(bulletsUsed);
    }


    // Get summary scores text to show user once the game finished
    void getScores(){
      string totalScore = "Total score: " + _score.ToString("F2");
      string totalLights = "Succeeded hits: " + _nLight.Count.ToString("F0");
      string meanPunct = "Mean of points scored: " + _nPoints.Average().ToString("F2");
      string maxPunct = "Best shot points: " + _nPoints.Max().ToString("F2");
      string minPunct = "Worst shot points: " + _nPoints.Min().ToString("F2");
      string meanBullets = "Mean of bullets used to hit: " + _nBulletsToHit.Average().ToString("F2");
      string meanTime = "Mean time to hit target since light on: " + _nTimeToHit.Average().ToString("F2");
      StaticClass.scoreText = totalScore +
                              System.Environment.NewLine + 
                              totalLights +
                              System.Environment.NewLine + 
                              meanPunct + 
                              System.Environment.NewLine + 
                              maxPunct + 
                              System.Environment.NewLine + 
                              minPunct + 
                              System.Environment.NewLine + 
                              meanBullets + 
                              System.Environment.NewLine +
                              meanTime;
    }
}
