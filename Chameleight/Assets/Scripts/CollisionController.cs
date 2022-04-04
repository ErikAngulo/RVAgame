using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public ButtonController buttonController;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "Button") // If A collides with B
        {
            buttonController.Pushed(theCollision.gameObject,this.gameObject);
            
        }
    }
}
