using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleSignalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignalConsoleStart(GameObject prefab)
    {
        if(ConsoleContentLoader.instance != null)
        {
            ConsoleContentLoader.instance.OpenConsole(prefab);
        }
    }
}
