using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    //Process a collision.
    void OnTriggerEnter(Collider theCollider){
         //Check if the object that has collided with the limit barrier is a ball.
         if(theCollider.gameObject.tag == "Ball_throw"){
             //Process ball collision.
             theCollider.gameObject.GetComponent<CollisionController>().OutCollision();
         }
    }
}
