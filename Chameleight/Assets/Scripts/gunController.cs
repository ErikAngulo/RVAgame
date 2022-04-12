using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public GameObject dart;
    public float dartSpeed = 20.0f;

    private Vector3 _angles = Vector3.zero;
    private float _max_angle = 60.0f;
    
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space) | Input.GetMouseButtonDown(1)){
            FireDart();
        }
        if(Input.GetMouseButton(0)){
            Cursor.lockState = CursorLockMode.Locked;
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");
            _angles.y += rotateHorizontal * sensitivity;
            _angles.y = Mathf.Clamp(_angles.y,-_max_angle,_max_angle);
            _angles.x -= rotateVertical * sensitivity;
            _angles.x = Mathf.Clamp(_angles.x,-_max_angle,_max_angle);

            gameObject.transform.rotation = Quaternion.Euler(_angles);
        }else{
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FireDart(){
        GameObject newDart = Instantiate(dart);
        newDart.gameObject.GetComponentInChildren<dartController>().setAsTemporal();
        newDart.transform.position = transform.position;
        newDart.transform.rotation = transform.rotation;
        newDart.GetComponent<Rigidbody>().velocity = -newDart.transform.forward * dartSpeed;
    }
}
