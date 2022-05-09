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
    public IOController ioController;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _latestHitText;
    private TextMeshProUGUI _remainingTime;
    
    // PlaneLight Initialization
    public Light blueLight;
    public Light orangeLight;
    public bool movement;
    public AudioSource soundLight;
    private Light _choosedLight;
    public float playTime;
    // Scores
    private float _score = 0;
    //private bool _enabled = false;
     
    // List to choose random light for initilization
    private List<Light> _optionLight = new List<Light>();
    private float wait = -1.0f;
    private bool _collision = false;
    private string _scoreScene = "GameOverScene";

    private gunController guncontroller;
    private bool _finished = false;
    private float _timeNeededToHit = 0.0f;
    private List<string> _nLight = new List<string>();
    private List<float> _nTimeToHit = new List<float>();
    private List<float> _nCoordX = new List<float>();
    private List<float> _nCoordY = new List<float>();
    private List<float> _nPoints = new List<float>();
    private List<int> _nBulletsToHit = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _latestHitText = GameObject.Find("LatestHitText").GetComponent<TextMeshProUGUI>();
        _remainingTime = GameObject.Find("RemainingTimeText").GetComponent<TextMeshProUGUI>();

        _optionLight.Add(blueLight);
        _optionLight.Add(orangeLight);
        int index = UnityEngine.Random.Range(0, _optionLight.Count);
        _choosedLight = _optionLight[index];
        _choosedLight.enabled = true;

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
      if (wait <= 0 && _collision){
            int index = UnityEngine.Random.Range(0, _optionLight.Count);
            _choosedLight = _optionLight[index];
            _choosedLight.enabled = true;
            _collision = false;
            soundLight.Play();
            
            _timeNeededToHit = 0.0f;
      }
      playTime -= Time.deltaTime;
      _remainingTime.text = "Time: " + playTime.ToString("F1") + "s";
      if (playTime < 0.0f && !_finished){
        _finished = true;
        ioController.WriteStatistics2(_nLight, _nTimeToHit, _nCoordX, _nCoordY, _nPoints, _nBulletsToHit);
        getScores();
        GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(_scoreScene);
      }

      _timeNeededToHit += Time.deltaTime;            
    }

    public void TargetHit(Light lightTargetTouched, float _points, Vector3 _coord){
        //coordX and coordY considering centre of target is 0,0
        _score += _points;
        _scoreText.text = "Score: " + _score.ToString("F2");
        _latestHitText.text = "Latest: " + _points.ToString("F2");

       lightTargetTouched.enabled = false;
       _collision = true;
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
      _nTimeToHit.Add(_timeNeededToHit);
      _nCoordX.Add(_coord.x);
      _nCoordY.Add(_coord.y);
      _nPoints.Add(_points);
      int bulletsUsed = guncontroller.dartsFiredAndReset();
      _nBulletsToHit.Add(bulletsUsed);
    }


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
