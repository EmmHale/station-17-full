using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleContentLoader : MonoBehaviour
{
    public Animator consoleAnimator;

    public GameObject content;

    private bool isOpen = false;

    public static ConsoleContentLoader instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two player consoles!");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Opens the UI console
    public void OpenConsole(GameObject content_prefab)
    {
        Debug.Log("Opening console window");

        if (!isOpen)
        {
            StartCoroutine(LoadContent(content_prefab));
            PlayerInteract.instance.StartLongPopup();
            isOpen = true;
            PlayerInteract.instance.playerLook.lockCamera = true;
        }
    }

    //Closes the console 
    public void CloseConsole()
    {
        Debug.Log("Closing console window");

        if (isOpen)
        {
            StartCoroutine(UnLoadContent());
            PlayerInteract.instance.StopLongPopup();
            isOpen = false;
            PlayerInteract.instance.playerLook.lockCamera = false;
        }
    }

    //Loads the prefab of the content
    private IEnumerator LoadContent(GameObject content_prefab)
    {
        consoleAnimator.SetTrigger("Start");

        //load prefab
        GameObject new_content = Instantiate(content_prefab, content_prefab.transform.position, content_prefab.transform.rotation) as GameObject;
        new_content.transform.SetParent(content.transform, false);

        yield return new WaitForSeconds(2);

        //do other stuff
    }

    //Unloads the content from the popup
    private IEnumerator UnLoadContent()
    {
        consoleAnimator.SetTrigger("Close");

        //disable content

        yield return new WaitForSeconds(0);

        //unLoad prefab

        foreach (Transform child in content.transform)
        {
            if (child != content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
