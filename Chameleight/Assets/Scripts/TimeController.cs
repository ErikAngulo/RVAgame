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

    //Initialize timers.
    public void Start(){
        generalTimeText.text = "Time: " + totalTime.ToString("0.0") + "s";
        _generalTime = Time.time;
        _timeReaction = _generalTime;
        _timeable = true;
        //Initial phase is the reaction phase.
        _timeableReaction = true;
    }

    //Update the general timer.
    public void Update(){
        float act_time;
        //Check if the game has not ended (there is still time left).
        if(_timeable){
            if(totalTime>0.0f){
                act_time = totalTime-(Time.time-_generalTime);
            }else{
                act_time = Time.time-_generalTime;
            }
            //Update general time text.
            generalTimeText.text = "Time: " + act_time.ToString("0.0") + "s";
            //Check if the remaining time is 0.
            if(totalTime>0.0f && act_time<=0.0f){
                //Store time information.
                ResultsTime();
                //Write the statistics of the game.
                ioController.WriteStatistics1();
                //End game, change scene.
                GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(_scoreScene);
            }
        }
    }

    //Store time information and disable the timers.
    public void ResultsTime(){
        //Disable the timers.
        _timeable = false;
        _timeableReaction = false;
        _timeableDecision = false;
        //Compute statistics.
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

        //Store the statistics in a static class.
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

    //Update the decision and reaction timers.
    public void UpdateTime(){
        //Check if the current phase is reaction phase.
        if(_timeableReaction){
            //Add reaction time.
            _timesReactionList.Add((phase,Time.time-_timeReaction));
            //Change to decision phase.
            _timeableReaction = false;
            _timeReaction = 0.0f;
            _timeableDecision = true;
            _timeDecision = Time.time;
        //Check if the current phase is decision phase.
        }else if(_timeableDecision){
            //Add decision time.
            _timesDecisionList.Add((phase,Time.time-_timeDecision));
            //Change to reaction phase.
            _timeableDecision = false;
            _timeDecision = 0.0f;
            _timeableReaction = true;
            _timeReaction= Time.time;
            phase += 1;
        }
    }

    //Add throw time.
    public void AddThrowTime(float _timeThrow, int ballNumber){
        _timesThrowList.Add((ballNumber,Time.time-_timeThrow));
    }

    //Get reaction time list.
    public List<(int,float)> GetReaction(){
        return _timesReactionList;
    }

    //Get decision time list.
    public List<(int,float)> GetDecision(){
        return _timesDecisionList;
    }

    //Get throw time list.
    public List<(int,float)> GetThrow(){
        return _timesThrowList;
    }

    //Get total time.
    public float GetTotal(){
        if(totalTime>0.0f){
            return totalTime;
        }else{
            return Time.time-_generalTime;
        }
    }
}
