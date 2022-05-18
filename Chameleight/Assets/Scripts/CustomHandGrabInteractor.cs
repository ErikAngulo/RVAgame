using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandPosing;

public class CustomHandGrabInteractor : HandGrabInteractor
{
    public HoldController holdController;
    public string hand;

    protected override void InteractableSelected(HandGrabInteractable interactable)
    {
        base.InteractableSelected(interactable);
        holdController.SetHand(hand);
    }
}