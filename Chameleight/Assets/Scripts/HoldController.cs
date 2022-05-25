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

    private int _limit = 0;
    private int _collisioned = 0;
    private int _number = 0;
    private GameObject _ball;
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _localScale;
    private string hand;

    private string _scoreScene = "GameOverScene";

    public void Start(){
        _limit = totalLimit-1;
        ballsText.text = "Balls: " + _limit;
        Copy(initialBall.transform);
    }

    public void SetHand(string h){
        hand = h;
    }

    public void Limit(){
        _collisioned = _collisioned + 1;
        if(totalLimit > 0 && _collisioned >= totalLimit){
            timeController.ResultsTime();
            ioController.WriteStatistics1();
            GameObject.Find("UIButtonControl").GetComponent<ButtonHandler>().ChangeScene(_scoreScene);
        }
    }

    public void Pickup(GameObject go)
     {
            _ball = go;
            
            _ball.GetComponent<CollisionController>().SetNumber(_number);
            _ball.GetComponent<Rigidbody>().useGravity = true;

            _number += 1;

            timeController.UpdateTime();
     }

     private void Copy(Transform t){
         _position = t.position;
         _rotation = t.rotation;
         _localScale = t.localScale;
     }

     public void Throw(){
        if((totalLimit==0 ||_limit>0) && _ball.tag != "Ball_throw"){
            if(totalLimit>0){_limit -=1;}
            GameObject newBall = Instantiate(prefab);
            newBall.GetComponent<CollisionController>().timeController = _ball.GetComponent<CollisionController>().timeController;
            newBall.GetComponent<CollisionController>().scoreController = _ball.GetComponent<CollisionController>().scoreController;
            newBall.GetComponent<CollisionController>().buttonController = _ball.GetComponent<CollisionController>().buttonController;
            newBall.GetComponent<CollisionController>().holdController = _ball.GetComponent<CollisionController>().holdController;
            newBall.GetComponent<CustomTransformer>().holdController = _ball.GetComponent<CustomTransformer>().holdController;
            newBall.transform.position = _position;
            newBall.transform.rotation = _rotation;
            newBall.transform.localScale = _localScale;
            ballsText.text = "Balls: " + _limit;
        }
        _ball.GetComponent<GrabInteractable>().enabled = false;
        _ball.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
        _ball.GetComponent<CollisionController>().StartTime();
        if(hand=="right"){
            _ball.GetComponent<Rigidbody>().velocity = rightHandAnchor.GetComponent<VelocityController>().GetSpeed();
        }else if(hand=="left"){
            _ball.GetComponent<Rigidbody>().velocity = leftHandAnchor.GetComponent<VelocityController>().GetSpeed();
        }
        _ball.tag = "Ball_throw";
        throwStatisticController.AddSpeed(_ball.GetComponent<Rigidbody>().velocity.magnitude/StaticClass.BallFactor,_number);
        throwStatisticController.AddAngle(_ball.GetComponent<Rigidbody>().velocity.normalized,_number);
        timeController.UpdateTime();
        StartCoroutine(DestroyTime(destroyTime,_ball));
     }

     IEnumerator DestroyTime(float time, GameObject go)
    {
        bool col = go.GetComponent<CollisionController>().Collisionable();
        while(col){
            col = go.GetComponent<CollisionController>().Collisionable();
            yield return null;
        }

        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Destroy(go);
    }

}
