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

    private void Start(){
        lastPosition = transform.position;
    }

    private void FixedUpdate()
    {   
        //Debug.Log("LASTPOSITION");
        //Debug.Log(lastPosition.ToString("F5"));
        //Debug.Log("CURRENTPOSITION");
        //Debug.Log(transform.position.ToString("F5"));
        //Debug.Log("DISTANCE");
        //Debug.Log((transform.position-lastPosition).ToString("F5"));
        if(OVRInput.GetActiveController() == OVRInput.Controller.Touch || OVRInput.GetActiveController() == controller){
            speed.Add(OVRInput.GetLocalControllerVelocity(controller));
        }else if(OVRInput.GetActiveController() == OVRInput.Controller.Hands || OVRInput.GetActiveController() == hand && (reliable.IsDataValid && reliable.IsDataHighConfidence)){
            speed.Add((transform.position-lastPosition)/Time.fixedDeltaTime);
            //speed.Add(OVRInput.GetLocalControllerVelocity(hand));
        }else if(speed.Count-1>=0){
            speed.Add(speed[speed.Count-1]);
        }
        if(speed.Count>maxSize){
            speed.RemoveAt(0);
        }
        lastPosition = transform.position;
    }

    public Vector3 GetSpeed(){
        Vector3 finalSpeed = new Vector3(0,0,0);
        float total = 0.0f;
        for(int i = 0; i < speed.Count; i++){
            total = total + i;
        }
        for(int i = 0; i < speed.Count; i++){
            finalSpeed = finalSpeed+speed[i]*(i/total);
        }
        Debug.Log("Factor:"+velFactor.ToString());
        return velFactor*finalSpeed;
    }
}
