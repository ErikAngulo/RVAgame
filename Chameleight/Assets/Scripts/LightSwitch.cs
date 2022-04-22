using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    public Light lightBO;
    public gameController gameController;
    private float _maxPoints = 10.0f;


    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        string _dart = "Dart";
        float _points = 0.0f;
         
        if (collision.gameObject.tag.Equals(_dart) && lightBO.enabled) 
        {
            //If the GameObject has the same tag as specified:

            // get collision of me/this
            BoxCollider boxcoll = gameObject.GetComponent<BoxCollider>();

            // get collision contact point (world space)
            ContactPoint contact = collision.GetContact(collision.contactCount - 1);
            contact = collision.GetContact(0);
            Vector3 pos = contact.point;

            // get collider centre (world space)
            Vector3 colliderCenter = boxcoll.bounds.center;

            // transform collision point to local space relative to collider center (with needed position and rotation changes)
            Vector3 rotation = gameObject.transform.rotation.eulerAngles;
            Vector3 transformed = pos - colliderCenter;
            transformed = Quaternion.Euler(-rotation.x, -rotation.y, -rotation.z) * transformed;
            
            // calculate distance (in x and y respect centre (0,0,0))
            float distance = Mathf.Sqrt(Mathf.Pow(transformed.x, 2) + Mathf.Pow(transformed.y, 2));
            
            // as it is a symetric dartboard in box collider, we will get only points inside its diameter (not corners)
            float diameter = boxcoll.size.x / 2 * transform.localScale.x;
            if (distance == 0.0f){ // avoid division by 0 (center) in next step
                _points = _maxPoints;
                gameController.TargetHit(lightBO, _points);
            }
            else if (distance <= diameter){
                // _maxPoints variable will be if hit in center, else progresively decline until diameter (0 points)
                // in diameter: maxPoints - declineStep * diameter = 0 --> declineStep = maxPoints / diameter
                float declineStep = _maxPoints / diameter;
                _points = _maxPoints - declineStep * distance;
                gameController.TargetHit(lightBO, _points);
            }
            // do nothing if collision but not inside dartboard (distance < diameter)
            
        }
        
    }    


}
