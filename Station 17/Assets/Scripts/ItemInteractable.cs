/************************************
 * Author: Emmett Hale
 * 
 * Purpose: Item interactable
 ************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : Interactable
{
    public Item item;
    public Vector3 defaultPosition;
    public Quaternion defaultRotation;

    public void Start()
    {
        defaultPosition = transform.position;
        defaultRotation = transform.rotation;
    }

    public override bool PreformAction()
    {
        base.PreformAction();

        if(PlayerInventory.instance != null)
        {
            PlayerInventory.instance.Add(item);
            gameObject.SetActive(false);
        }

        return true;
    }

    public void ResetPosition()
    {
        transform.position = defaultPosition;
        transform.rotation = defaultRotation;
    }


}
