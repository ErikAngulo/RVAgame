using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI generalTimeText;
    public TextMeshProUGUI resultsText;
    public CameraController cameraController;
    public float totalTime = 30.0f;
    private float _generalTime = 0.0f;
    private float _time = 0.0f;
    private bool _timeable = false;

    private List<float> _timesList = new List<float>();

    public void Start(){
        generalTimeText.text = "Time: " + totalTime.ToString("0.0") + "s";
    }

    public void InitTime(){
        _generalTime = Time.time;
        _time = Time.time;
        _timeable = true;
    }

    public void Update(){
        if(_timeable){
            float act_time = Time.time-_time;
            timeText.text = "Reaction: " + act_time.ToString("0.0") + "s";
            act_time = totalTime-(Time.time-_generalTime);
            generalTimeText.text = "Time: " + act_time.ToString("0.0") + "s";
            if(act_time<=0.0f){
                _timeable = false;
                float total = 0.0f;
                foreach(float d in _timesList)
                {    
                    total += d;
                }
                if(_timesList.Count>0){
                    total /= _timesList.Count;
                }
                resultsText.text = "Average reaction time: " + total.ToString("0.0") + "s";
                cameraController.EndGame();
            }
        }
    }

    public void UpdateTime(){
        if(_timeable){
            _timesList.Add(Time.time-_time);
            _time = Time.time;
            timeText.text = "Reaction: 0.0s";
        }
    }
}
