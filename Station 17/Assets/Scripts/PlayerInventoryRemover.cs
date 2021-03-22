using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryRemover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveItem(Item item)
    {
        if (PlayerInventory.instance)
        {
            PlayerInventory.instance.Remove(item);
        }
        else
        {
            Debug.Log("No player inventory");
        }
    }
}
