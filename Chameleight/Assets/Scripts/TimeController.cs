using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI reactionTimeText;
    public TextMeshProUGUI decisionTimeText;
    public TextMeshProUGUI generalTimeText;
    public TextMeshProUGUI resultsText;
    public CameraController cameraController;
    public float totalTime = 0.0f;
    private float _generalTime = 0.0f;
    private float _timeReaction = 0.0f;
    private float _timeDecision = 0.0f;
    private bool _timeable = false;
    private bool _timeableReaction = false;
    private bool _timeableDecision = false;

    private List<float> _timesReactionList = new List<float>();
    private List<float> _timesDecisionList = new List<float>();

    public void Start(){
        generalTimeText.text = "Time: " + totalTime.ToString("0.0") + "s";
        _generalTime = Time.time;
        _timeReaction = _generalTime;
        _timeable = true;
        _timeableReaction = true;
    }

    public void Update(){
        float act_time;
        if(_timeableReaction){
            act_time = Time.time-_timeReaction;
            reactionTimeText.text = "Reaction: " + act_time.ToString("0.0") + "s";
        }
        if(_timeableDecision){
            act_time = Time.time-_timeDecision;
            decisionTimeText.text = "Decision: " + act_time.ToString("0.0") + "s";
        }
        if(_timeable){
            if(totalTime>0.0f){
                act_time = totalTime-(Time.time-_generalTime);
            }else{
                act_time = Time.time-_generalTime;
            }
            generalTimeText.text = "Time: " + act_time.ToString("0.0") + "s";
            if(totalTime>0.0f && act_time<=0.0f){
                ResultsTime();
                cameraController.EndGame();
            }
        }
    }

    public void ResultsTime(){
        _timeable = false;
        _timeableReaction = false;
        _timeableDecision = false;
        float totalReaction = 0.0f;
        foreach(float d in _timesReactionList)
        {    
            totalReaction += d;
        }
        if(_timesReactionList.Count>0){
            totalReaction /= _timesReactionList.Count;
        }
        string text = "Reaction: " + totalReaction.ToString("0.0") + "s";
        float totalDecision = 0.0f;
        foreach(float d in _timesDecisionList)
        {    
            totalDecision += d;
        }
        if(_timesDecisionList.Count>0){
            totalDecision /= _timesDecisionList.Count;
        }
        text += " Decision: " + totalDecision.ToString("0.0") + "s";
        resultsText.text = text;
    }

    public void UpdateTime(){
        if(_timeableReaction){
            _timesReactionList.Add(Time.time-_timeReaction);
            _timeableReaction = false;
            _timeReaction = 0.0f;
            reactionTimeText.text = "Reaction: 0.0s";
            _timeableDecision = true;
            _timeDecision = Time.time;
        }else if(_timeableDecision){
            _timesDecisionList.Add(Time.time-_timeDecision);
            _timeableDecision = false;
            _timeDecision = 0.0f;
            decisionTimeText.text = "Decision: 0.0s";
            _timeableReaction = true;
            _timeReaction= Time.time;
        }
    }
}
