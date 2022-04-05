using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float maxDistance = 4.0f;
    public float waitTime = 0.2f;
    public GameObject Object;
    public ButtonController buttonController;
    public HoldController holdController;
    

    private Vector3 _angles = Vector3.zero;
    private bool _waiting = false;
    private bool _end = false;
    // Update is called once per frame
    void Update()
    {
        if(!_end){
            Cursor.lockState = CursorLockMode.Locked;
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");

            _angles.y += rotateHorizontal * sensitivity;
            _angles.x -= rotateVertical * sensitivity;

            gameObject.transform.rotation = Quaternion.Euler(_angles);

            List<string> tags = new List<string>();
            tags.Add("Button");
            tags.Add("Ball");
    
            RaycastHit hit;

            // if raycast hits, it checks if it hit an object with the tag Player
            if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) && tags.IndexOf(hit.collider.tag) != -1)
            {
                Object.GetComponent<UnityEngine.UI.Image>().color= Color.green;
                if(Input.GetMouseButtonDown(0) && hit.collider.tag == "Button"){
                    buttonController.Pushed(hit.collider.gameObject,null);
                    _waiting = false;
                    StartCoroutine(Interact(waitTime));
                }else if(Input.GetMouseButtonDown(0) && hit.collider.tag == "Ball"){
                    holdController.Try(hit.collider.gameObject);
                    _waiting = false;
                    StartCoroutine(Interact(waitTime));
                }
            }else{
                Object.GetComponent<UnityEngine.UI.Image>().color= Color.red;
            }
        }
    }

    public void EndGame(){
        _end = true;
    }

    IEnumerator Interact(float waitTime)
    { 
        if(!_waiting){
            Object.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
            _waiting = true;
        }
        yield return new WaitForSeconds(waitTime);
        Object.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
    }

}
