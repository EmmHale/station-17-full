/*********************************************
 * Author: Emmett Hale
 * 
 * Purpose: Data class for floor save data
 ********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorData
{
    public RoomData[] rooms;

    public bool isValid = true;

    public int floorID;

    public FloorData(int ID)
    {
        SetFloorData(ID);
    }

    public FloorData()
    {
        isValid = false;
    }

    public void SetFloorData(int ID)
    {
        rooms = new RoomData[RoomManager.rooms.Count];

        int i = 0;
        foreach (RoomManager room in RoomManager.rooms)
        {
            SetRoomData(i, room.MakeSaveData());
            i++;
        }

        floorID = ID;
    }

    public void SetRoomData(int index, RoomData value)
    {
        rooms[index] = value;
    }

    public RoomData GetRoomData(int index)
    {
        return rooms[index];
    }

    public int RoomCount()
    {
        return rooms.Length;
    }
}
