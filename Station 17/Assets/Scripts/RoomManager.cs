/************************************
 * Author: Emmett Hale
 * 
 * Purpose: Manages room data
 ************************************/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RoomManager : MonoBehaviour
{
    public static List<RoomManager> rooms;
    public List<Enemy> enemyList = new List<Enemy>();

    bool roomActive = false;

    //public List<Interactable> managedInteractions = new List<Interactable>();
    public Interactable[] managedInteractables;

    [Tooltip("Identifing name for a room manager. This is used to match save data and should only be changed with care.")]
    public string identifier = null;

    public void Start()
    {
        //Enable singleton
        if(rooms != null)
        {
            if(rooms[0] == null)
            {
                rooms = new List<RoomManager>();
            }

            rooms.Add(this);
        }
        else
        {
            rooms = new List<RoomManager>();
            rooms.Add(this);
            rooms = rooms.OrderBy(i => i.identifier).ToList<RoomManager>();

            Debug.Log("Room made: " + identifier);
        }

        //Remember to set identifier. This value is used to 
        //match up save data
        if(identifier == null)
        {
            Debug.LogWarning("Not set room manager identifier. Please set!", gameObject);
        }
    }

    private float timeDeavtivateTrigger = 5;
    private bool deactivating = false;
    private float timeSince = 0;
    private void Update()
    {
        //Delay turn enemies off for timeDeavtivateTrigger
        if (deactivating)
        {
            if(timeSince >= timeDeavtivateTrigger)
            {
                Debug.Log("Deactivating");
                foreach (Enemy enemy in enemyList)
                {
                    enemy.DeactivateEnemy();
                    enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
                    enemy.gameObject.GetComponent<NavMeshAgent>().ResetPath();
                    enemy.gameObject.GetComponent<NavMeshAgent>().Warp(enemy.defaultPosition);
                    enemy.transform.rotation = enemy.defaultRotation;
                }
                deactivating = false;
                timeSince = 0;
            }
            else
            {
                timeSince += Time.deltaTime;
            }
        }
    }
    public void ActivateRoom()
    {
        if(!roomActive)
        {
            roomActive = true;

            //Activate enemyList
            foreach(Enemy enemy in enemyList)
            {
                enemy.ActivateEnemy();
            }
            //Activate Triggers?

            deactivating = false;
            timeSince = 0;
        }
    }




    //Turn room off
    public void DeactivateRoom()
    {
        if(roomActive)
        {
            roomActive = false;

            deactivating = true;

            foreach (Enemy enemy in enemyList)
            {
                enemy.DeactivateEnemy();
                enemy.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
    }

    //Save a rooms progression
    public void SaveRoom()
    {
        foreach (Interactable item in managedInteractables)
        {
            if (item.completed && !item.saved)
            {
                item.saved = true;
            }
        }
    }

    //Reset unsaved progress
    public void PlayerDead()
    {
        foreach (Interactable item in managedInteractables)
        {
            if (item.completed && !item.saved)
            {
                item.PlayerDied();
            }
        }
    }


    //Create save data for a save file
    public RoomData MakeSaveData()
    {
        RoomData temp = new RoomData(managedInteractables.Length);

        for(int i = 0; i < managedInteractables.Length; i++)
        {
            temp.SetSaveState(i, managedInteractables[i].completed);
        }

        temp.ID = identifier;

        return temp;
    }

    public GameObject lights;

    private bool light_signal_working = true;
    public void TurnOffLights()
    {
        if(light_signal_working)
        {
            if(lights)
                lights.SetActive(false);
        }
    }

    public void TurnOnLights()
    {
        if(light_signal_working)
        {
            if (lights)
                lights.SetActive(true);
        }
    }

    public void SetLightSignals(bool value)
    {
        light_signal_working = value;
    }
}
