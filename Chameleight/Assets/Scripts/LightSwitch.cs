using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    public Light lightBO;
    public gameController gameController;
    private float _points = 10.0f;


    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        string _dart = "Dart";
         
        if (collision.gameObject.tag.Equals(_dart) && lightBO.enabled) 
        {
            //If the GameObject has the same tag as specified:
            gameController.TargetHit(lightBO, _points);
        }
        
    }    


}
