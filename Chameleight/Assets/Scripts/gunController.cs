using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    public float sensitivity = 5.0f;
    public GameObject dart;
    public float dartSpeed = 20.0f;
    public AudioSource shootSound;

    private Vector3 _angles = Vector3.zero;
    private float _max_angle = 60.0f;
    private int _dartsUsed = 0;
    private bool _fired = false;
    
    // Update is called once per frame
    void Update()
    {
        if(OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.5f && !_fired){
            FireDart();
            _fired = true;
        }
        else if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) < 0.5f){
            _fired = false;
        }
        else{
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FireDart(){
        shootSound.Play();
        GameObject newDart = Instantiate(dart);
        newDart.gameObject.GetComponentInChildren<dartController>().setAsTemporal();
        newDart.transform.position = transform.position;
        newDart.transform.rotation = transform.rotation;
        newDart.GetComponent<Rigidbody>().velocity = -newDart.transform.forward * dartSpeed;
        _dartsUsed += 1;
    }

    public int dartsFiredAndReset(){
        int used = _dartsUsed;
        _dartsUsed = 0;
        return (used);
    }
}
