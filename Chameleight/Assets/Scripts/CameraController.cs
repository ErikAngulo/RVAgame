using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public float maxDistance = 10.0f;
    public float waitTime = 0.2f;
    public GameObject Object;
    public ButtonController buttonController;

    private Vector3 _angles = Vector3.zero;
    private bool _waiting = false;

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        _angles.y += rotateHorizontal * sensitivity;
        _angles.x -= rotateVertical * sensitivity;

        gameObject.transform.rotation = Quaternion.Euler(_angles);

         RaycastHit hit;
 
         // if raycast hits, it checks if it hit an object with the tag Player
         if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance) && hit.collider.tag == "Button")
         {
             Object.GetComponent<UnityEngine.UI.Image>().color= Color.blue;
             if(Input.GetMouseButtonDown(0)){
                 buttonController.Pushed(hit.collider.gameObject);
                 _waiting = false;
                 StartCoroutine(Push(waitTime));
             }
         }else{
             Object.GetComponent<UnityEngine.UI.Image>().color= Color.red;
         }
    }

    IEnumerator Push(float waitTime)
    { 
        if(!_waiting){
            Object.GetComponent<RectTransform>().sizeDelta = new Vector2(5, 5);
            _waiting = true;
        }
        yield return new WaitForSeconds(waitTime);
        Object.GetComponent<RectTransform>().sizeDelta = new Vector2(10, 10);
    }
}
