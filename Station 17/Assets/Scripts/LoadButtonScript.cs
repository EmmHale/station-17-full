using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadButtonScript : MonoBehaviour
{
    public PlayerSaveData data;
    public static List<LoadButtonScript> buttons;

    // Start is called before the first frame update
    void Start()
    {
        if (buttons != null)
        {
            if (buttons[0] == null)
            {
                buttons = new List<LoadButtonScript>();
            }

            buttons.Add(this);
        }
        else
        {
            buttons = new List<LoadButtonScript>();
            buttons.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayOptions()
    {
        LoadUIManager.instance.DisplaySaveOptionMenu(data);
    }
}
