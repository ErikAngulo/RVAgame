using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoldController : MonoBehaviour
{
    public Camera cam;
    public TimeController timeController;
    public CameraController cameraController;
    public ThrowStatisticController throwStatisticController;
    public IOController ioController;
    public Transform guide;
    public float ballSpeed = 20.0f;
    public float destroyTime = 1.0f;
    public int totalLimit = 10;
    public TextMeshProUGUI ballsText;

    private int _limit = 0;
    private int _number = 0;
    private GameObject _ball;
    private bool _holdable = true;
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _localScale;
    private bool _end = false;

    void Start(){
        if(totalLimit>0){
            _limit = totalLimit;
            ballsText.text = "Balls: " + _limit;
        }else{
            ballsText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_end){
            if(!_holdable && Input.GetMouseButtonDown(1)){
                Throw();
            }else if(!_holdable && (totalLimit <= 0 || _limit > 0)){
                _ball.transform.position = guide.position;
            }
        }
    }

    public void EndGame(){
        _end = true;
    }

    public void Try(GameObject go){
        if(_holdable){
            Pickup(go);
        }
    }

    public void Limit(){
        if(totalLimit > 0 &&_limit<=0){
            timeController.ResultsTime();
            cameraController.EndGame();
            ioController.Write();
        }
    }

    private void Pickup(GameObject go)
     {
            _ball = go;
            //We set the object parent to our guide empty object.
            _ball.transform.SetParent(guide);
            
            _ball.GetComponent<CollisionController>().SetNumber(_number);
            _number += 1;
    
            //Set gravity to false while holding it
            _ball.GetComponent<Rigidbody>().useGravity = false;
            _ball.GetComponent<Collider>().enabled = false;
            Copy(_ball.transform);
    
            //we apply the same rotation our main object (Camera) has.
            _ball.transform.localRotation = transform.rotation;
            //We re-position the ball on our guide object 
            _ball.transform.position = guide.position;
    
            _holdable = false;

            timeController.UpdateTime();
     }

     private void Copy(Transform t){
         _position = t.position;
         _rotation = t.rotation;
         _localScale = t.localScale;
     }

     private void Throw(){
        GameObject newBall = Instantiate(_ball);
        newBall.transform.position = _position;
        newBall.transform.rotation = _rotation;
        newBall.transform.localScale = _localScale;
        newBall.GetComponent<Rigidbody>().useGravity = true;
        newBall.GetComponent<Collider>().enabled = true;
        _ball.GetComponent<CollisionController>().StartTime();
        _ball.GetComponent<Rigidbody>().useGravity = true;
        _ball.GetComponent<Rigidbody>().isKinematic = false;
        _ball.GetComponent<Collider>().enabled = true;
        _ball.transform.position = cam.transform.position;
        _ball.GetComponent<Rigidbody>().velocity = ballSpeed*cam.transform.forward;
        Debug.Log(ballSpeed);
        Debug.Log(cam.transform.forward.ToString());
        _ball.tag = "Ball_throw";
        guide.GetChild(0).parent = null;
        timeController.UpdateTime();
        throwStatisticController.AddSpeed(ballSpeed,_number);
        throwStatisticController.AddAngle(cam.transform.forward,_number);
        if(totalLimit>0){
            _limit -=1;
            ballsText.text = "Balls: " + _limit;
            if(_limit>0){
                _holdable = true;
            }else{
                EndGame();
            }
        }else{
            _holdable = true;
        }
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
