using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{

    // The gun of shooting game is controlled with this script
    // Its objective is to detect controller input and fire darts
    public float sensitivity = 5.0f;
    public GameObject dart;
    public float dartSpeed = 40.0f;
    public AudioSource shootSound;

    private Vector3 _angles = Vector3.zero;
    private int _dartsUsed = 0;
    private bool _fired = false;
    private float _triggerValue = 0.0f;
    
    // Update is called once per frame
    // Detect controller input and fire darts
    void Update()
    {
        // Detect trigger value (controller input) of L or R controller according to selected controller (L or R)
        if (StaticClass.Controller.Equals("Right")){
            _triggerValue = OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger);
        }else{
            _triggerValue = OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);
        }

        // Gun will only fire each time the trigger is pressed
        // That means that mantaining the trigger pressed only fires once. User has to release and press again to fire
        if(_triggerValue > 0.5f && !_fired){
            // Dart is fired
            FireDart();
            _fired = true;
        }
        // Save user released button
        else if (_triggerValue < 0.5f){
            _fired = false;
        }
        else{
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // Fire the dart from the gun
    private void FireDart(){
        // Play gun shooted effect sound
        shootSound.Play();
        // Create a new dart object
        GameObject newDart = Instantiate(dart);
        // Set the dart temporal, it will have lifetime
        newDart.gameObject.GetComponentInChildren<dartController>().setAsTemporal();
        // Put the dart at the same position and rotation of gun (simulate it is shooted from inside with same rotation)
        newDart.transform.position = transform.position;
        newDart.transform.rotation = transform.rotation;
        // Adjust dart velocity
        newDart.GetComponent<Rigidbody>().velocity = -newDart.transform.forward * dartSpeed;
        // Increment used darts
        _dartsUsed += 1;
    }

    // This function is called once the target is hit
    // In that case, we return how many darts the user needed to hit the dartboard
    // Then, we reset darts used to 0 to count again until correct target is hit
    public int dartsFiredAndReset(){
        int used = _dartsUsed;
        _dartsUsed = 0;
        return (used);
    }
}
