using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GInventory
{
    public List<GameObject> items = new List<GameObject>();

    public void AddItem(GameObject item)
    {
        items.Add(item);
    }

    public GameObject FindItemWithTag(string t)
    {
        foreach (GameObject item in items)
        {
            if (item.tag == t)
                return item;
        }
        return null;
    }

    public void RemoveItem(GameObject i)
    {
        if(items.Contains(i))
            items.Remove(i);
    }
}
