using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    private bool in_screen = true;

    public bool inScreen
    {
        get { return in_screen; }

        set { 
            in_screen = value;
            if(value == false && !PlayerMovement.instance.isMoveable())
            {
                PlayerMovement.instance.ToggleMovement();
            }
        }
    }

    public static TitleScreenManager instance;
    private CanvasGroup groupcontrol;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two title Managers!");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        groupcontrol = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if(inScreen && PlayerMovement.instance.isMoveable())
        {
            PlayerMovement.instance.ToggleMovement();

            PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = true;

            PlayerInteract.instance.Controllable = false;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void DisableTitleScreen()
    {
        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = false;

        PlayerInteract.instance.Controllable = true;

        inScreen = false;

        groupcontrol.interactable = false;
        groupcontrol.blocksRaycasts = false;
        groupcontrol.alpha = 0;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EnableTitleScreen()
    {
        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = true;

        PlayerInteract.instance.Controllable = false;

        inScreen = true;

        groupcontrol.interactable = true;
        groupcontrol.blocksRaycasts = true;
        groupcontrol.alpha = 1;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ReturnToMain()
    {
        StartCoroutine(PlayerLoadManager.instance.LoadFloor(0, 0, true));
        StartCoroutine(PausePlayerActions());
    }
    public IEnumerator PausePlayerActions()
    {
        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = true;

        PlayerInteract.instance.Controllable = false;

        yield return new WaitForSeconds(5);

        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = false;

        PlayerInteract.instance.Controllable = true;
    }
}
