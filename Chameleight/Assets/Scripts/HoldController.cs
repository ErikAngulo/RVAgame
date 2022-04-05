using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldController : MonoBehaviour
{
    public Camera cam;
    public TimeController timeController;
    public ButtonController buttonController;
    public Transform guide;
    public float ballSpeed = 20.0f;
    public float destroyTime = 1.0f;

    private GameObject _ball;
    private bool _holdable = true;
    private bool _first = true;
    private Vector3 _position;
    private Quaternion _rotation;
    private Vector3 _localScale;

    // Update is called once per frame
    void Update()
    {
        if(!_holdable && Input.GetMouseButtonDown(1)){
            Throw();
        }else if(!_holdable){
            _ball.transform.position = guide.position;
        }
    }

    public void Try(GameObject go){
        if(_holdable){
            Pickup(go);
        }
    }
    private void Pickup(GameObject go)
     {

            _ball = go;
            //We set the object parent to our guide empty object.
            _ball.transform.SetParent(guide);
    
            //Set gravity to false while holding it
            _ball.GetComponent<Rigidbody>().useGravity = false;
            _ball.GetComponent<Collider>().enabled = false;
            Copy(_ball.transform);
    
            //we apply the same rotation our main object (Camera) has.
            _ball.transform.localRotation = transform.rotation;
            //We re-position the ball on our guide object 
            _ball.transform.position = guide.position;
    
            _holdable = false;
            
            if(_first){
                _first = false;
                timeController.InitTime();
            }
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
        _ball.GetComponent<Rigidbody>().useGravity = true;
        _ball.GetComponent<Rigidbody>().isKinematic = false;
        _ball.GetComponent<Collider>().enabled = true;
        _ball.transform.position = cam.transform.position;
        _ball.GetComponent<Rigidbody>().velocity = ballSpeed*cam.transform.forward;
        _ball.tag = "Ball_throw";
        guide.GetChild(0).parent = null;
        _holdable = true;
        StartCoroutine(DestroyTime(destroyTime,_ball));
     }

     IEnumerator DestroyTime(float time, GameObject go)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(go.GetComponent<CollisionController>().getMissed()){
            buttonController.Missed();
        }
        Destroy(go);
    }

}
