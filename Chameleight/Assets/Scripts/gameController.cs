using System.Collections;
using System.Collections.Generic;
//using System;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    // PlaneLight Initialization
    public Light blueLight;
    public Light orangeLight;
    private Light _choosedLight;
    // Scores
    private float _score = 0;
    //private bool _enabled = false;
     
    // List to choose random light for initilization
    private List<Light> _optionLight = new List<Light>();
    private float wait = -1.0f;
    private bool _collision = false;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _optionLight.Add(blueLight);
        _optionLight.Add(orangeLight);
        int index = Random.Range(0, _optionLight.Count);
        _choosedLight = _optionLight[index];
        _choosedLight.enabled = true;
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
      }
                      
    }

    public void TargetHit(Light lightTargetTouched, float _points){
        _score += _points;
        scoreText.text = "Score: " + _score;

       lightTargetTouched.enabled = false;
       _collision = true;
       wait = Random.Range(0.5f,5.0f);
    }
}
