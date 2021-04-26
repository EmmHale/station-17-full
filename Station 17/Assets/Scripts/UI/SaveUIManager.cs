using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveUIManager : MonoBehaviour
{
    public static SaveUIManager instance;

    public GameObject scrollContent;

    public GameObject buttonPrefab;

    public GameObject saveText;

    public GameObject saveInputPopup;

    public GameObject overwriteNameText;

    public GameObject overwriteSavePopup;

    bool displaying_submenu = false;

    string currentName = "";

    int currentSaveID = -1;

    private List<GameObject> buttons;

    PlayerSaveData currentData;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two save ui Managers!");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSaveMenu(int saveID, string description)
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<CanvasGroup>().interactable = true;

        CreateButtons(PlayerLoadManager.instance.LoadAllFiles());

        currentSaveID = saveID;

        saveText.GetComponent<TextMeshProUGUI>().text = description;

        PlayerInteract.instance.ClearPlayerInteractionStuff();

        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = true;

        PlayerInteract.instance.Controllable = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //Creates Buttons for Save menu UI
    public void CreateButtons(List<PlayerSaveData> data_list)
    {
        buttons = new List<GameObject>();

        foreach (PlayerSaveData data in data_list)
        {
            GameObject button = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(scrollContent.transform, false);
            button.GetComponent<SaveButtonScript>().data = data;

            buttons.Add(button);

            Transform tempButton;
            tempButton = button.transform.Find("Save Name");
            

            if (tempButton)
            {
                tempButton.GetComponent<TextMeshProUGUI>().text = data.saveName;
            }

            tempButton = button.transform.Find("Location Text");

            if (tempButton)
            {
                tempButton.GetComponent<TextMeshProUGUI>().text = data.floorName;
            }

            tempButton = button.transform.Find("Date Text");

            if (tempButton)
            {
                tempButton.GetComponent<TextMeshProUGUI>().text = data.saveTime.ToString("MMMM dd, yyyy");
            }
        }
    }

    //Clears the Menus buttons
    //Used after saving or closing 
    public void ClearButtons()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;

        /*if (SaveButtonScript.buttons != null)
        {
            foreach (SaveButtonScript button in SaveButtonScript.buttons)
            {
                Destroy(button);
            }
        }*/

        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
    }

    public void DisplaySaveNaming()
    {
        if (!displaying_submenu)
        {
            saveInputPopup.GetComponent<CanvasGroup>().alpha = 1;
            saveInputPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
            saveInputPopup.GetComponent<CanvasGroup>().interactable = true;

            displaying_submenu = true;
        }
    }

    public void CloseSaveNaming()
    {
        saveInputPopup.GetComponent<CanvasGroup>().alpha = 0;
        saveInputPopup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        saveInputPopup.GetComponent<CanvasGroup>().interactable = false;

        displaying_submenu = false;
    }

    public void DisplaySaveOverwrite(PlayerSaveData data)
    {
        if (!displaying_submenu)
        {
            overwriteSavePopup.GetComponent<CanvasGroup>().alpha = 1;
            overwriteSavePopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
            overwriteSavePopup.GetComponent<CanvasGroup>().interactable = true;

            currentName = data.saveName;
            overwriteNameText.GetComponent<TextMeshProUGUI>().text = currentName;

            displaying_submenu = true;

            currentData = data;
        }
    }

    public void CloseSaveOverwrite()
    {
        overwriteSavePopup.GetComponent<CanvasGroup>().alpha = 0;
        overwriteSavePopup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        overwriteSavePopup.GetComponent<CanvasGroup>().interactable = false;

        displaying_submenu = false;
    }

    public void SetCurrentName(string name)
    {
        currentName = name;
    }

    public void WriteSave()
    {
        PlayerLoadManager.instance.SavePlayerData(currentName, currentSaveID);
    }

    public void OverwriteSave()
    {
        Debug.Log("Deleting: " + currentData.saveName + " " + currentData.floorName + " " + currentData.saveTime + " ");
        PlayerLoadManager.instance.DeleteFile(currentData);

        PlayerLoadManager.instance.SavePlayerData(currentName, currentSaveID);
    }

    public void CloseSaveMenu()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;

        ClearButtons();

        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = false;

        PlayerInteract.instance.Controllable = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
