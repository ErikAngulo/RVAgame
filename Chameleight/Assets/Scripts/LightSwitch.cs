using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    private GameObject light;
    private int points;
    public Light lightBO;
    private bool _enabled = false;


    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        string _dart = "Dart";
        if (collision.gameObject.tag.Equals(_dart) && !_enabled)
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            lightBO.enabled = true;
            _enabled = true;
            Destroy(collision.gameObject);
        }
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        else if (collision.gameObject.tag.Equals(_dart) && _enabled) 
        {
            //If the GameObject has the same tag as specified, output this message in the console
            lightBO.enabled = false;
            _enabled = false;
            Destroy(collision.gameObject);
        }
        
    }

    
}
