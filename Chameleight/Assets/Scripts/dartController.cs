using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dartController : MonoBehaviour
{
    public gameController gameController;
    private float _lifeTime = 5.0f;
    private bool _temporal = false;

    void Update(){
        _lifeTime -= Time.deltaTime;
        if (_temporal && _lifeTime <= 0){
            Destroy(this.transform.parent.gameObject);
        }
    }
    void OnCollisionEnter(Collision obj){
        gameController.TargetHit(obj.gameObject);
    }
    public void setAsTemporal(){
        _temporal = true;
    }
}
