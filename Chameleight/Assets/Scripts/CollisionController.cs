using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{

    public TimeController timeController;
    public ButtonController buttonController;
    public HoldController holdController;
    public ScoreController scoreController;
    public AudioSource hitBall;
    private bool _collisionable = true;
    private int _ballNumber = 0;
    private float _timeThrow = 0.0f;

    //Process a collision.
    void OnCollisionEnter(Collision theCollision)
    {
        //Play the "hit" sound.
        hitBall.Play();
        //Check if the ball can collide.
        if(_collisionable){
            //Check if the ball has collided with a button.
            if(theCollision.gameObject.tag == "Button")
            {
                //Push the button.
                buttonController.Pushed(theCollision.gameObject,this.gameObject,_ballNumber);
                _collisionable = false;
                //Update timer.
                EndTime();
                //Check the ball limit.
                holdController.BallLimit();
            //Check if the ball has collided with the panel.
            }else if(theCollision.gameObject.tag == "Panel"){
                //Missed.
                scoreController.Missed(_ballNumber,this.gameObject.GetComponent<Renderer>().material.name);
                _collisionable = false;
                //Update timer.
                EndTime();
                //Check the ball limit.
                holdController.BallLimit();
            //Check if the ball has collided with the floor.
            }else if(theCollision.gameObject.tag == "Floor"){
                //Out.
                scoreController.Out(_ballNumber,this.gameObject.GetComponent<Renderer>().material.name);
                _collisionable = false;
                //Update timer.
                EndTime();
                //Check the ball limit.
                holdController.BallLimit();
            }
        }
    }

    //Set initial throw timer.
    public void StartTime(){
        _timeThrow = Time.time;
    }

    //Update throw timer.
    public void EndTime(){
        timeController.AddThrowTime(_timeThrow,_ballNumber);
    }

    //Set number of ball.
    public void SetNumber(int ball){
        _ballNumber = ball;
    }

    //Process a collision (just when the ball collides with a limit barrier).
    public void OutCollision(){
        //Check if the ball can collide.
         if(_collisionable){
            //Out.
            scoreController.Out(_ballNumber,this.gameObject.GetComponent<Renderer>().material.name);
            _collisionable = false;
            //Update timer.
            EndTime();
            //Check the ball limit.
            holdController.BallLimit();
         }
    }

    //Update whether the ball can collide.
    public bool Collisionable(){
        return _collisionable;
    }
}
