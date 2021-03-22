using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveButtonScript : MonoBehaviour
{
    public PlayerSaveData data;
    public static List<SaveButtonScript> buttons;

    // Start is called before the first frame update
    void Start()
    {
        if (buttons != null)
        {
            if (buttons[0] == null)
            {
                buttons = new List<SaveButtonScript>();
            }

            buttons.Add(this);
        }
        else
        {
            buttons = new List<SaveButtonScript>();
            buttons.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayOverwrite()
    {
        SaveUIManager.instance.DisplaySaveOverwrite(data);
    }
}
