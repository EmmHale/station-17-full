/*********************************************
 * Author: Emmett Hale
 * 
 * Purpose: Data class for room save states
 * 
 * The bool values stored is whether any given
 * interactable in a room has had its progress 
 * saved.
 * 
 * This value should map directly to the value
 * in the RoomManager's list of interacables.
 * 
 ********************************************/
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;

[System.Serializable]
public class RoomData
{
    public bool[] interactableSaveState;

    private string id;

    public string ID
    {
        get { return id; }
        set { id = value; }
    }

    public RoomData(int count)
    {
        interactableSaveState = new bool[count];
    }

    public void SetSaveState(int index, bool value)
    {
        interactableSaveState[index] = value;
    }

    public bool GetSaveState(int index)
    {
        return interactableSaveState[index];
    }
}
