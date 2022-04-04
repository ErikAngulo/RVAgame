using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldController : MonoBehaviour
{
    public Camera cam;
    public Transform guide;
    public float ballSpeed = 10.0f;

    private GameObject _ball;
    private bool _holdable = true;
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
    
            //we apply the same rotation our main object (Camera) has.
            _ball.transform.localRotation = transform.rotation;
            //We re-position the ball on our guide object 
            _ball.transform.position = guide.position;
    
            _holdable = false;
     }

     private void Throw(){
        _ball.GetComponent<Rigidbody>().useGravity = true;
        _ball.GetComponent<Collider>().enabled = true;
        _ball.transform.position = cam.transform.position;
        _ball.GetComponent<Rigidbody>().velocity = ballSpeed*cam.transform.forward;
        guide.GetChild(0).parent = null;
        _holdable = true;
     }

}
