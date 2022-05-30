using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction;
using Oculus.Interaction.HandPosing;

public class HoldController : MonoBehaviour
{
    public TimeController timeController;
    public ThrowStatisticController throwStatisticController;
    public GameObject rightHandAnchor;
    public GameObject leftHandAnchor;
    public IOController ioController;
    public float ballSpeed = 20.0f;
    public float destroyTime = 1.0f;
    public int totalLimit = 10;
    public TextMeshProUGUI ballsText;
    public GameObject initialBall;
    public GameObject forward;
    public GameObject prefab;
    public AudioSource pickBall;
    public AudioSource throwBall;

    private int _limit = 0;
    private int _collisioned = 0;
    private int _number = 0;
    private GameObject _ball;
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _localScale;
    private string hand;

    private string _scoreScene = "GameOverScene";

    //Initialization.
    public void Start(){
        //Initialize the number of remaining balls.
        _limit = totalLimit-1;
        ballsText.text = "Balls: " + _limit;
        //Copy the position of the initial ball.
        Copy(initialBall.transform);
    }

    //Store the position-rotation-scale of the given transform object.
     private void Copy(Transform t){
         _position = t.position;
         _rotation = t.rotation;
         _localScale = t.localScale;
     }

    //Set the hand that is currently holding the ball.
    public void SetHand(string h){
        hand = h;
    }

    //Check if the game has ended.
    public void BallLimit(){
        //Update the number of balls that have collided.
        _collisioned = _collisioned + 1;

        //Check if the number of balls that have collided is equal to the total number of balls.
        if(totalLimit > 0 && _collisioned >= totalLimit){
            //Store time information.
            timeController.ResultsTime();
            //Write the statistics of the game.
            ioController.WriteStatistics1();
            //End game, change scene.
            GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(_scoreScene);
        }
    }

    //Pick up the ball.
    public void Pickup(GameObject go)
     {
            //Play the pick up sound.
            pickBall.Play();
            _ball = go;
            
            //Set the current ball number.
            _ball.GetComponent<CollisionController>().SetNumber(_number);
            //Activate gravity.
            _ball.GetComponent<Rigidbody>().useGravity = true;

            _number += 1;

            //Update timer.
            timeController.UpdateTime();
     }

    //Throw the ball.
     public void Throw()
     {
        //Check if the number of remaining balls is not 0.
        if((totalLimit==0 ||_limit>0) && _ball.tag != "Ball_throw"){
            //Update number of remaining balls.
            if(totalLimit>0){_limit -=1;}
            //Create a new ball.
            GameObject newBall = Instantiate(prefab);
            //Initialize the new ball.
            newBall.GetComponent<CollisionController>().timeController = _ball.GetComponent<CollisionController>().timeController;
            newBall.GetComponent<CollisionController>().scoreController = _ball.GetComponent<CollisionController>().scoreController;
            newBall.GetComponent<CollisionController>().buttonController = _ball.GetComponent<CollisionController>().buttonController;
            newBall.GetComponent<CollisionController>().holdController = _ball.GetComponent<CollisionController>().holdController;
            newBall.GetComponent<CollisionController>().hitBall = _ball.GetComponent<CollisionController>().hitBall;
            newBall.GetComponent<CustomTransformer>().holdController = _ball.GetComponent<CustomTransformer>().holdController;
            //Set the position-rotation-scale of the new ball.
            newBall.transform.position = _position;
            newBall.transform.rotation = _rotation;
            newBall.transform.localScale = _localScale;
            ballsText.text = "Balls: " + _limit;
        }

        //Ensure that the old ball can't be grabbed again.
        _ball.GetComponent<GrabInteractable>().enabled = false;
        _ball.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
        
        //Initialize throw timer.
        _ball.GetComponent<CollisionController>().StartTime();
        
        //Set throw speed according to the speed of the hand that is holding the ball.
        if(hand=="right"){
            _ball.GetComponent<Rigidbody>().velocity = rightHandAnchor.GetComponent<VelocityController>().GetSpeed();
        }else if(hand=="left"){
            _ball.GetComponent<Rigidbody>().velocity = leftHandAnchor.GetComponent<VelocityController>().GetSpeed();
        }
        _ball.tag = "Ball_throw";
        
        //Store the throw statistics.
        throwStatisticController.AddSpeed(_ball.GetComponent<Rigidbody>().velocity.magnitude/StaticClass.BallFactor,_number);
        throwStatisticController.AddAngle(_ball.GetComponent<Rigidbody>().velocity.normalized,_number);
        
        //Update timer.
        timeController.UpdateTime();
        //Play throw sound.
        throwBall.Play();
        //Activate the countdown to destroy the old ball.
        StartCoroutine(DestroyTime(destroyTime,_ball));
     }

    //Count down to destroy the ball.
     IEnumerator DestroyTime(float time, GameObject go)
    {
        //Loop as long as the ball has not collided.
        bool col = go.GetComponent<CollisionController>().Collisionable();
        while(col){
            col = go.GetComponent<CollisionController>().Collisionable();
            yield return null;
        }

        //Loop during destroyTime seconds.
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //Destroy the ball object.
        Destroy(go);
    }

}
