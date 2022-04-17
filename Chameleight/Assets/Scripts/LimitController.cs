using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitController : MonoBehaviour
{
    void OnTriggerEnter(Collider theCollider){
         Debug.Log(theCollider.gameObject.tag);
         if(theCollider.gameObject.tag == "Ball_throw"){
             theCollider.gameObject.GetComponent<CollisionController>().Out();
         }
    }
}
