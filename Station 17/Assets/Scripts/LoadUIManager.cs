using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadUIManager : MonoBehaviour
{
    public GameObject loadOptionsPopup;

    public GameObject saveNameText;

    public PlayerSaveData currentData;

    bool displaying_submenu = false;

    private List<GameObject> buttons;

    public static LoadUIManager instance;

    public GameObject scrollContent;

    public GameObject buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("Two load ui Managers!");
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

    public void OpenLoadMenu()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<CanvasGroup>().interactable = true;

        CreateButtons(PlayerLoadManager.instance.LoadAllFiles());

        PlayerInteract.instance.ClearPlayerInteractionStuff();

        PlayerMovement.instance.ToggleMovement();

        PlayerMovement.instance.gameObject.GetComponentInChildren<LookScript>().lockCamera = true;

        PlayerInteract.instance.Controllable = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void CloseLoadMenu()
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

    //Creates Buttons for load menu UI
    public void CreateButtons(List<PlayerSaveData> data_list)
    {
        buttons = new List<GameObject>();

        foreach (PlayerSaveData data in data_list)
        {
            GameObject button = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(scrollContent.transform, false);
            button.GetComponent<LoadButtonScript>().data = data;

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
    //Used after loading a save or closing 
    public void ClearButtons()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;

        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
    }

    public void DeleteSave()
    {
        PlayerLoadManager.instance.DeleteFile(currentData);
    }

    public void LoadSave()
    {
        PlayerLoadManager.instance.LoadFromPlayerData(currentData);
    }

    public void DisplaySaveOptionMenu(PlayerSaveData data)
    {
        if (!displaying_submenu)
        {
            loadOptionsPopup.GetComponent<CanvasGroup>().alpha = 1;
            loadOptionsPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
            loadOptionsPopup.GetComponent<CanvasGroup>().interactable = true;

            saveNameText.GetComponent<TextMeshProUGUI>().text = data.saveName;

            displaying_submenu = true;

            currentData = data;
        }
    }

    public void CloseSaveOptionsMenu()
    {
        loadOptionsPopup.GetComponent<CanvasGroup>().alpha = 0;
        loadOptionsPopup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        loadOptionsPopup.GetComponent<CanvasGroup>().interactable = false;

        displaying_submenu = false;
    }
}
