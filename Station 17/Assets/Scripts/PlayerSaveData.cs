/*********************************************
 * Author: Emmett Hale
 * 
 * Purpose: Manages Player loading, saving,
 * and updating player progress
 * 
 ********************************************/
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSaveData 
{
    //current number of playable floors in game
    public const int NUM_FLOORS = 5;

    //Names of all items in inventory
    public List<string> inventory;

    //Name of save
    public string saveName = "";

    //Array of saved floor data
    public FloorData[] gameStateData = new FloorData[NUM_FLOORS];

    //id of floor currently on
    public int floorID = 0;

    //id of save location
    public int saveLocationID = 0;

    //time saved
    public DateTime saveTime;

    //name of floor
    public string floorName;

    //Pass the inventory you wish to save, name of save, and save point id
    public PlayerSaveData(List<string> new_inventory, string name, int savePointID)
    {
        inventory = new_inventory;
        saveName = name;
        saveLocationID = savePointID;
        saveTime = DateTime.Now;

        floorID = SceneManager.GetActiveScene().buildIndex;

        floorName = SceneManager.GetActiveScene().name;
    }

    public void SetDate(DateTime time)
    {
        saveTime = time;
    }

    public void SetFloor(int index, FloorData floor)
    {
        gameStateData[index] = floor;
    }

    public FloorData GetFloor(int index)
    {
        return gameStateData[index];
    }
}
