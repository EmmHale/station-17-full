using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleSubContentScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendCloseSignal()
    {
        if(ConsoleContentLoader.instance != null)
        {
            ConsoleContentLoader.instance.CloseConsole();
        }
    }
}
