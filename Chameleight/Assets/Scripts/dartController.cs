using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dartController : MonoBehaviour
{
    // Script to control dart lifetime
    // The dart, which is shoot from the gun, has a lifetime of 5 seconds
    // After 5 seconds, the dart is destroyed (it disappears from scene)
    // The original dart is not temporal, so new darts can be instanciated
    
    public gameController gameController;
    private float _lifeTime = 5.0f;
    private bool _temporal = false;

    void Update(){
        _lifeTime -= Time.deltaTime;
        if (_temporal && _lifeTime <= 0){
            Destroy(this.transform.parent.gameObject);
        }
    }

    public void setAsTemporal(){
        _temporal = true;
    }

    public void changeLifetime(float lifetime){
        _lifeTime = lifetime;
    }
}
