using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public ButtonController buttonController;
    private bool _miss = true;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "Button") // If A collides with B
        {
            buttonController.Pushed(theCollision.gameObject,this.gameObject);
            _miss = false;
        }
    }

    public bool getMissed() => _miss;
}
