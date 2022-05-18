using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public TextMeshProUGUI generalTimeText;
    public HoldController holdController;
    public IOController ioController;
    public ScoreController scoreController;
    public float totalTime = 0.0f;
    private float _generalTime = 0.0f;
    private float _timeReaction = 0.0f;
    private float _timeDecision = 0.0f;
    private bool _timeable = false;
    private bool _timeableReaction = false;
    private bool _timeableDecision = false;
    private int phase = 0;

    private List<(int,float)> _timesReactionList = new List<(int,float)>();
    private List<(int,float)> _timesDecisionList = new List<(int,float)>();
    private List<(int,float)> _timesThrowList = new List<(int,float)>();

    private string _scoreScene = "GameOverScene";

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
        }
        if(_timeableDecision){
            act_time = Time.time-_timeDecision;
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
                GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(_scoreScene);
            }
        }
    }

    public void ResultsTime(){
        _timeable = false;
        _timeableReaction = false;
        _timeableDecision = false;
        float totalReaction = 0.0f;
        foreach(var d in _timesReactionList)
        {    
            totalReaction += d.Item2;
        }
        if(_timesReactionList.Count>0){
            totalReaction /= _timesReactionList.Count;
        }
        string reactionText = "Mean Reaction time: " + totalReaction.ToString("0.0") + "s";
        float totalDecision = 0.0f;
        foreach(var d in _timesDecisionList)
        {    
            totalDecision += d.Item2;
        }
        if(_timesDecisionList.Count>0){
            totalDecision /= _timesDecisionList.Count;
        }
        string decisionText = "Mean Decision time: " + totalDecision.ToString("0.0") + "s";
        float totalThrow = 0.0f;
        foreach(var d in _timesThrowList)
        {    
            totalThrow += d.Item2;
        }
        if(_timesThrowList.Count>0){
            totalThrow /= _timesThrowList.Count;
        }
        string throwText = "Mean Throw time: " + totalThrow.ToString("0.0") + "s";

        StaticClass.scoreText = reactionText +
                              System.Environment.NewLine + 
                              decisionText +
                              System.Environment.NewLine + 
                              throwText + 
                              System.Environment.NewLine + 
                              scoreController.correctText.text + 
                              System.Environment.NewLine + 
                              scoreController.incorrectText.text + 
                              System.Environment.NewLine + 
                              scoreController.missedText.text + 
                              System.Environment.NewLine +
                              scoreController.outText.text;
    }

    public void UpdateTime(){
        if(_timeableReaction){
            _timesReactionList.Add((phase,Time.time-_timeReaction));
            _timeableReaction = false;
            _timeReaction = 0.0f;
            _timeableDecision = true;
            _timeDecision = Time.time;
        }else if(_timeableDecision){
            _timesDecisionList.Add((phase,Time.time-_timeDecision));
            _timeableDecision = false;
            _timeDecision = 0.0f;
            _timeableReaction = true;
            _timeReaction= Time.time;
            phase += 1;
        }
    }

    public void AddThrowTime(float _timeThrow, int ballNumber){
        _timesThrowList.Add((ballNumber,Time.time-_timeThrow));
    }

    public List<(int,float)> GetReaction(){
        return _timesReactionList;
    }

    public List<(int,float)> GetDecision(){
        return _timesDecisionList;
    }

    public List<(int,float)> GetThrow(){
        return _timesThrowList;
    }

    public float GetTotal(){
        if(totalTime>0.0f){
            return totalTime;
        }else{
            return Time.time-_generalTime;
        }
    }
}
