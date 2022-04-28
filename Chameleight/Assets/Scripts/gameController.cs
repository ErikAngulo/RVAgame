using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
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
    private string scoreScene = "GameOverScene";
    
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
        int index = Random.Range(0, _optionLight.Count);
        _choosedLight = _optionLight[index];
        _choosedLight.enabled = true;

        dartboardController scriptOrange = GameObject.Find("orange").GetComponent<dartboardController>();
        scriptOrange.movement = movement;
        dartboardController scriptBlue = GameObject.Find("blue").GetComponent<dartboardController>();
        scriptBlue.movement = movement;
    }

    // Update is called once per frame
    void Update()
    {  
      wait -= Time.deltaTime;
      if (wait <= 0 && _collision){
            int index = Random.Range(0, _optionLight.Count);
            _choosedLight = _optionLight[index];
            _choosedLight.enabled = true;
            _collision = false;
            soundLight.Play();
      }
      playTime -= Time.deltaTime;
      _remainingTime.text = "Time: " + playTime.ToString("F1") + "s";
      if (playTime < 0.0f){
        GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(scoreScene);
      }
                      
    }

    public void TargetHit(Light lightTargetTouched, float _points){
        _score += _points;
        _scoreText.text = "Score: " + _score.ToString("F2");
        _latestHitText.text = "Latest: " + _points.ToString("F2");

       lightTargetTouched.enabled = false;
       _collision = true;
       wait = Random.Range(0.5f,5.0f);
    }
}
