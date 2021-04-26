using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenDoorButtonInteractable : Interactable
{
    public GameObject HiddenDoor;
    Animator DoorAnimator;
    void Start()
    {
        DoorAnimator = HiddenDoor.GetComponent<Animator>();
        interactText = "E: Push Button";
    }
    public override bool PreformAction()
    {
        Debug.Log("Moving Door...");
        if (!isCharge)
        {
            if (DoorAnimator.GetBool("IsOpening"))
            {

                DoorAnimator.SetBool("IsOpening", false);
                DoorAnimator.SetBool("IsClosing", true);
                interactText = "E: Push Button";
            }
            else
            {
                DoorAnimator.SetBool("IsOpening", true);
                DoorAnimator.SetBool("IsClosing", false);
                interactText = "E: Push Button";
            }
        }

        return base.PreformAction();
    }

    public override void CompleteAction()
    {
        Debug.Log("Moving Door...");
        if (DoorAnimator.GetBool("IsOpening"))
        {

            DoorAnimator.SetBool("IsOpening", false);
            DoorAnimator.SetBool("IsClosing", true);
            interactText = "E: Push Button";
        }
        else
        {
            DoorAnimator.SetBool("IsOpening", true);
            DoorAnimator.SetBool("IsClosing", false);
            interactText = "E: Push Button";
        }

        base.CompleteAction();
    }
}
