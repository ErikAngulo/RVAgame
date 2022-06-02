using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dartboardController : MonoBehaviour
{

    // Move the target at shooting game each time it is hit

    // If the dartboard is wanted to be moved
    public bool movement = true;
    // The duration of movement from origin to target position
    float _lerpDuration = 2.0f;
    // Time elapsed since movement started
    float _timeElapsed = 2.0f;

    Vector3 _startPosition;
    Vector3 _targetPosition;

    Vector3 _originalLocation;

    // How many we want to move from dartboard original location at its local space at X and Y axis
    float _randomMoveX = 1.5f;
    float _randomMoveY = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = gameObject.transform.position;
        _originalLocation = new Vector3(pos.x, pos.y, pos.z);
        _targetPosition = new Vector3(pos.x, pos.y, pos.z);
        // Each dartboard object has a LightSwitch script to detect collision with that dartboard
        // dartboardController script must be also attached to dartboard object
        // Since a script cannot be referenced in another script without a game object,
        // we assign this (dartboardController) to LightSwitch dartboard Controller paramenetr manually
        LightSwitch script = gameObject.GetComponentInChildren<LightSwitch>();
        script.dartboardController = this;
    }

    // Update is called once per frame
    void Update()
    {
        // If lerp duration is not yet achieved, we continue moving the dartboard object to target location
        if (_timeElapsed < _lerpDuration)
        {
            // Move dartboard from origin to target position
            // Third parameter 0 is start and 1 is target position
            // The position at each elapsed time is interpolated according to number (more elapsed time, number bigger, nearer the target pos)
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timeElapsed / _lerpDuration);
            _timeElapsed += Time.deltaTime;
        }
    }

    // Script to change dartboard its position
    // Start position is saved from previous target position
    // Create a new target position randomly near original location
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
