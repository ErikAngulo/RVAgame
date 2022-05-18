using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class CustomGrabInteractor : GrabInteractor
{
    public HoldController holdController;
    public string hand;

    protected override void InteractableSelected(GrabInteractable interactable)
    {
        base.InteractableSelected(interactable);
        holdController.SetHand(hand);
    }
}
