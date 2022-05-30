using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityController : MonoBehaviour
{
    public float velFactor;
    public int maxSize;
    public OVRInput.Controller controller;
    public OVRInput.Controller hand;
    public OVRHand reliable;
    private Vector3 lastPosition;
    private List<Vector3> speed = new List<Vector3>();

    //Initialize last position of the hand.
    private void Start(){
        lastPosition = transform.position;
    }

    //Compute the current speed of the controllers/hands.
    private void FixedUpdate()
    {
        //Check if the controllers are active.
        if(OVRInput.GetActiveController() == OVRInput.Controller.Touch || OVRInput.GetActiveController() == controller){
            //Add controller speed.
            speed.Add(OVRInput.GetLocalControllerVelocity(controller));
        //Check if the hand tracking is active and the measures are reliable.
        }else if((OVRInput.GetActiveController() == OVRInput.Controller.Hands || OVRInput.GetActiveController() == hand) && reliable.IsDataValid && reliable.IsDataHighConfidence){
            //Add hand speed.
            speed.Add((transform.position-lastPosition)/Time.fixedDeltaTime);
        //Else.
        }else if(speed.Count-1>=0){
            //Duplicate last speed.
            speed.Add(speed[speed.Count-1]);
        }

        //Remove the oldest element if the speed list size is higher than maxSize.
        if(speed.Count>maxSize){
            speed.RemoveAt(0);
        }

        //Update last position of the hand.
        lastPosition = transform.position;
    }

    //Get a weighted mean of the last maxSize speeds.
    public Vector3 GetSpeed(){
        Vector3 finalSpeed = new Vector3(0,0,0);
        float total = 0.0f;
        for(int i = 0; i < speed.Count; i++){
            total = total + i;
        }
        //The weight of the i-th element in the list is i.
        for(int i = 0; i < speed.Count; i++){
            finalSpeed = finalSpeed+speed[i]*(i/total);
        }
        //Multiply the final speed by a specified factor.
        return velFactor*finalSpeed;
    }
}
