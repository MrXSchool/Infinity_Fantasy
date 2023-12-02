using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Item> items = new List<Item>();


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }


    public void Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Add(item);
            UseItem(item);


        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        items.RemoveAt(index);
    }

    public void UseItem(Item item)
    {
        if (item != null)
        {
            item.Use();
            EquiqmentManager.instance.Equiq(item as Equiqment);

        }


    }


}
