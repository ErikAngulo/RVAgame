using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dartboardController : MonoBehaviour
{

    public bool movement = true;
    float _lerpDuration = 2.0f;
    float _timeElapsed = 2.0f;

    Vector3 _startPosition;
    Vector3 _targetPosition;

    Vector3 _originalLocation;

    float _randomMoveX = 1.5f;
    float _randomMoveY = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = gameObject.transform.position;
        _originalLocation = new Vector3(pos.x, pos.y, pos.z);
        _targetPosition = new Vector3(pos.x, pos.y, pos.z);
        LightSwitch script = gameObject.GetComponentInChildren<LightSwitch>();
        script.dartboardController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeElapsed < _lerpDuration)
        {
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timeElapsed / _lerpDuration);
            _timeElapsed += Time.deltaTime;
        }
    }

    public void changePosition(){
        if (movement)
        {
            _startPosition = _targetPosition;
            float posX = Random.Range(_originalLocation.x - _randomMoveX, _originalLocation.x + _randomMoveX);
            float posY = Random.Range(_originalLocation.y - _randomMoveY, _originalLocation.y + _randomMoveY);
            float posZ = _originalLocation.z;
            _targetPosition = new Vector3(posX, posY, posZ);
            _timeElapsed = 0.0f;
        }
    }
}
