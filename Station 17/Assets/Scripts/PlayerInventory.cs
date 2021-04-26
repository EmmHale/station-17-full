/************************************
 * Author: Emmett Hale
 * 
 * Purpose: Manages the players 
 * inventory
 * 
 * Duplicate items are currently not allowed
 ************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public List<Item> items = new List<Item>();

    public void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Two inventory instance!");
            return;
        }

        instance = this;
    }

    public List<string> GetItemNames()
    {
        List<string> names = new List<string>();

        foreach(Item item in items)
        {
            names.Add(item.name);
        }

        return names;
    }

    public void SetItems(List<string> names)
    {
        items = new List<Item>();

        foreach(string name in names)
        {
            Item temp = Item.CreateInstance<Item>();

            temp.name = name;

            items.Add(temp);
        }
    }

    public void Add(Item item)
    {
        if (!items.Exists(x => x.name == item.name))
        {
            items.Add(item);
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public bool Search(Item item)
    {
        bool found = false;
        foreach(Item stuff in items)
        {
            if(stuff.name == item.name)
            {
                found = true;
            }
        }

        return found;
    }

    public void CleanInventory()
    {
        items.Clear();
    }
}
