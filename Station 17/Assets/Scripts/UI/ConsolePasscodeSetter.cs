using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsolePasscodeSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetToUse(Interactable toUse)
    {
        ConsolePasscodeScript.SetToUse(toUse);
    }

    public void SetString(string correct)
    {
        ConsolePasscodeScript.SetString(correct);
    }
}
