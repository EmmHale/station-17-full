/************************************
 * Author: Emmett Hale
 * 
 * Purpose: Base interactable class
 *  for objects the player interacts with
 ************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string interactText = "";
    public bool isCharge = false;
    public bool willFail = false;
    public string interactFailText = "";
    
    public float interactTime = 0;

    [SerializeField] public UnityEvent OnCompleteAction;
    [SerializeField] public UnityEvent OnPlayerDeath;

    public bool saved = false;
    public bool completed = false;

    public Item requiredItem;

    public virtual void SetFail(bool fail)
    {
        willFail = fail;
    }


    public virtual bool PreformAction()
    {
        if (requiredItem == null || (requiredItem != null && PlayerInventory.instance.Search(requiredItem)))
        {
            if (!isCharge && !willFail)
            {
                OnCompleteAction.Invoke();
                completed = true;
            }
        }
        else
        {
            return false;
        }

        return !willFail;
    }

    public virtual void ClearAction()
    {

    }


    //Used by charged actions
    public virtual void CompleteAction()
    {
        if(isCharge)
        {
            OnCompleteAction.Invoke();
            completed = true;
        }
    }

    public void SaveInteraction()
    {
        saved = true;
    }

    public virtual void PlayerDied()
    {
        if (!saved)
        {
            completed = false;
            OnPlayerDeath.Invoke();
        }
    }
}
