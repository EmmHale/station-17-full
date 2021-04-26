using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ConsolePasscodeScript : MonoBehaviour
{
    static public Interactable to_use_interactable;

    static public string correct_string;

    public TextMeshProUGUI input;

    [SerializeField] public UnityEvent OnCompleteAction;

    public void Checktext()
    {
        Debug.Log("Comparing ->" + Regex.Replace(correct_string.Trim(), "[^a-zA-Z0-9-/]", string.Empty) + 
            "<- to ->" + Regex.Replace(input.GetParsedText().Trim(), "[^a-zA-Z0-9-/]", string.Empty) + "<- is " + 
            string.Compare(Regex.Replace(correct_string.Trim(), "[^a-zA-Z0-9-/]", string.Empty),
                           Regex.Replace(input.GetParsedText().Trim(), "[^a-zA-Z0-9-/]", string.Empty)));

        if(string.Compare(Regex.Replace(correct_string.Trim(), "[^a-zA-Z0-9-/]", string.Empty), 
                           Regex.Replace(input.GetParsedText().Trim(), "[^a-zA-Z0-9-/]", string.Empty)) == 0)
        {
            OnCompleteAction.Invoke();

            to_use_interactable.PreformAction();
            to_use_interactable.CompleteAction();
        }
        else
        {
            if(AudioManager.instance)
            {
                AudioManager.instance.Play("Interaction Error");
            }
        }
    }

    static public void SetToUse(Interactable toUse)
    {
        to_use_interactable = toUse;
    }

    static public void SetString(string correct)
    {
        correct_string = correct;
    }
}
