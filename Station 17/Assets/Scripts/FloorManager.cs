using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public static FloorManager instance;

    public string floorName = "";

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null)
        {
            Debug.Log("Two Floor Managers!");
        }
        else
        {
            instance = this;
        }
    }
}
