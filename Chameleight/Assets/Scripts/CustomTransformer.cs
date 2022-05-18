using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Oculus.Interaction
{
public class CustomTransformer : MonoBehaviour, ITransformer
{
    public HoldController holdController;
    private IGrabbable _grabbable;
    private Pose _previousGrabPose;

    public void Initialize(IGrabbable grabbable)
    {
        _grabbable = grabbable;
    }

    public void BeginTransform()
    {
        Pose grabPoint = _grabbable.GrabPoints[0];
        _previousGrabPose = grabPoint;
        holdController.Pickup(_grabbable.Transform.gameObject);
    }

    public void UpdateTransform()
    {
        Pose grabPoint = _grabbable.GrabPoints[0];
        var targetTransform = _grabbable.Transform;

        Vector3 worldOffsetFromGrab = targetTransform.position - _previousGrabPose.position;
        Vector3 offsetInGrabSpace = Quaternion.Inverse(_previousGrabPose.rotation) * worldOffsetFromGrab;
        Quaternion rotationInGrabSpace = Quaternion.Inverse(_previousGrabPose.rotation) * targetTransform.rotation;

        targetTransform.position = (grabPoint.rotation * offsetInGrabSpace) + grabPoint.position;
        targetTransform.rotation = grabPoint.rotation * rotationInGrabSpace;

        _previousGrabPose = grabPoint;
    }

    public void EndTransform() {
        holdController.Throw();
    }
}
}
