using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;

    public GameObject prefab;

    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Debug.Log("inventory UIs!");
            Destroy(gameObject);
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

    bool is_enabled = false;


    public bool IsEnabled()
    {
        return is_enabled;
    }

    public void EnableUI()
    {
        CanvasGroup temp = GetComponent<CanvasGroup>();

        temp.alpha = 1;
        temp.blocksRaycasts = true;
        temp.interactable = true;
        CleanUI();
        FillUI();
        is_enabled = true;
    }

    public void DisableUI()
    {
        CanvasGroup temp = GetComponent<CanvasGroup>();

        temp.alpha = 0;
        temp.blocksRaycasts = false;
        temp.interactable = false;
        CleanUI();
        is_enabled = false;
    }

    List<GameObject> buttons = new List<GameObject>();

    void FillUI()
    {
        foreach(Item item in PlayerInventory.instance.items)
        {
            GameObject button = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            button.transform.SetParent(content.transform, false);

            buttons.Add(button);

            if(button)
            {
                button.GetComponent<TextMeshProUGUI>().text = item.name;
            }
        }
    }

    void CleanUI()
    {
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
    }
}
