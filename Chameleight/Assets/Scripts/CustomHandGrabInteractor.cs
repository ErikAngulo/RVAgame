using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandPosing;

public class CustomHandGrabInteractor : HandGrabInteractor
{
    public HoldController holdController;
    public string hand;

    //Custom InteractableSelected that is used to keep track of the hand that is holding the ball.
    protected override void InteractableSelected(HandGrabInteractable interactable)
    {
        base.InteractableSelected(interactable);
        //Set current hand.
        holdController.SetHand(hand);
    }
}