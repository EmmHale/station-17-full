using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTransitionScript : MonoBehaviour
{
    public int floorToTravelTo = 0;

    public int spawnPointTarget = 0;

    public void Travel()
    {
        PlayerMovement.instance.transform.parent = null;
        DontDestroyOnLoad(PlayerMovement.instance);

        PlayerLoadManager.instance.LoadFloorToFloor(floorToTravelTo, spawnPointTarget);
    }

    public void FastTravel()
    {
        PlayerMovement.instance.transform.parent = null;
        DontDestroyOnLoad(PlayerMovement.instance);

        PlayerLoadManager.instance.LoadFloorToFloor(floorToTravelTo, spawnPointTarget, true);
    }
}
