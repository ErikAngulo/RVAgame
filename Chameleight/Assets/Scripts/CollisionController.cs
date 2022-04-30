using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    public TimeController timeController;
    public ButtonController buttonController;
    public HoldController holdController;
    public ScoreController scoreController;
    private bool _collisionable = true;
    private int _ballNumber = 0;
    private float _timeThrow = 0.0f;

    // Start is called before the first frame update
    void OnCollisionEnter(Collision theCollision)
    {
        if(_collisionable){
            if(theCollision.gameObject.tag == "Button") // If A collides with B
            {
                buttonController.Pushed(theCollision.gameObject,this.gameObject,_ballNumber);
                _collisionable = false;
                EndTime();
                holdController.Limit();
            }else if(theCollision.gameObject.tag == "Panel"){
                scoreController.Missed(_ballNumber,this.gameObject.GetComponent<Renderer>().material.name);
                _collisionable = false;
                EndTime();
                holdController.Limit();
            }else if(theCollision.gameObject.tag == "Floor"){
                scoreController.Out(_ballNumber,this.gameObject.GetComponent<Renderer>().material.name);
                _collisionable = false;
                EndTime();
                holdController.Limit();
            }
        }
    }

    public void StartTime(){
        _timeThrow = Time.time;
    }

    public void EndTime(){
        timeController.AddThrowTime(_timeThrow,_ballNumber);
    }

    public void SetNumber(int ball){
        _ballNumber = ball;
    }

    public void Out(){
         if(_collisionable){
            scoreController.Out(_ballNumber,this.gameObject.GetComponent<Renderer>().material.name);
            _collisionable = false;
            EndTime();
            holdController.Limit();
         }
    }

    public bool Collisionable(){
        return _collisionable;
    }
}
