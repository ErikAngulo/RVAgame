using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    
    // Script to handle collision at shooting game

    // This script is attached to each dartboard target (which has its dartboardController)
    // The light associated to the target is also stored
    // If target collisioned and its light is ON, calculate points and continue game

    public Light lightBO;
    public gameController gameController;
    public dartboardController dartboardController;
    public GameObject bulletHole;
    public AudioSource normalHitSound;
    public AudioSource badHitSound;
    public AudioSource goodHitSound;
    private float _maxPoints = 10.0f;


    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {

        //Check for a match with the specified name on any GameObject that collides with your GameObject
        string _dart = "Dart";
        float _points = 0.0f;
        // If the target detected collision with a dart, only if associated light is ON
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
                drawBulletHoleAndSound(pos, _points);
                gameController.TargetHit(lightBO, _points, transformed);
                dartboardController.changePosition();
            }
            else if (distance <= diameter){
                // _maxPoints variable will be if hit in center, else progresively decline until diameter (0 points)
                // in diameter: maxPoints - declineStep * diameter = 0 --> declineStep = maxPoints / diameter
                float declineStep = _maxPoints / diameter;
                // calculate points
                _points = _maxPoints - declineStep * distance;
                // Show hole at hit position and play sound
                drawBulletHoleAndSound(pos, _points);
                // Tell shooting game controller a correct collision happened to continue game
                // (turn off light, save points, turn on again a light...)
                gameController.TargetHit(lightBO, _points, transformed);
                // Dartboard will change its position
                dartboardController.changePosition();
            }
            // do nothing if collision but not inside dartboard (distance < diameter)
            
        }
        
    }    

    // Draw a bullet hole at collisioned point and play sound according to scored points
    void drawBulletHoleAndSound(Vector3 pos, float points){
        // sound effect
        float lowPercentage = 0.25f;
        float highPercentage = 0.7f;
        if (points < lowPercentage * _maxPoints){
            badHitSound.Play();
        }
        else if (points > highPercentage * _maxPoints){
            goodHitSound.Play();
        }
        else {
            normalHitSound.Play();
        }
        // draw bullet hole
        GameObject newBulletHole = Instantiate(bulletHole);
        newBulletHole.gameObject.GetComponentInChildren<dartController>().changeLifetime(1.5f);
        newBulletHole.gameObject.GetComponentInChildren<dartController>().setAsTemporal();
        newBulletHole.transform.position = new Vector3(pos.x, pos.y, pos.z + 0.2f);
        newBulletHole.transform.SetParent(dartboardController.transform);
    }

}
