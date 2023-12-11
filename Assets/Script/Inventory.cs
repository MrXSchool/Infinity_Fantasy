using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<ScriptOBJ> items = new List<ScriptOBJ>();
    public GameObject HotBar;
    public int slotMax = 6;
    public bool update = false;
    public PlayerScript player;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory found!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        if (HotBar == null) { HotBar = GameObject.Find("HotBar"); }
        player = GetComponent<PlayerScript>();
    }


    public bool Add(ScriptOBJ item)
    {
        Debug.Log(item.typeOfItem.ToString());
        if (item.typeOfItem.ToString() != "AddMax")
        {
            if (items.Count > slotMax)
            {
                Debug.Log("Not enough room");
                return false;
            }
            if (items.Count > 0)
            {
                bool isExit = false;
                foreach (ScriptOBJ i in items)
                {

                    if (i.nameItem == item.nameItem)
                    {
                        i.amount += item.amount;
                        update = true;
                        isExit = true;
                    }

                }
                if (isExit == false)
                {
                    items.Add(item);
                    update = false;
                }
            }
            else
            {
                items.Add(item);
                update = false;

            }

            Debug.Log(items.Count);
            Debug.Log("update" + update);
            SetToHotBar(item, update);
        }
        return true;
    }

    public void Remove(ScriptOBJ item)
    {
        items.Remove(item);
    }

    public void RemoveAt(int index)
    {
        items.RemoveAt(index);
    }

    public void UseItem(ScriptOBJ item)
    {
        if (item != null)
        {
            item.Use();
            // EquiqmentManager.instance.Equiq(item as Equiqment);

        }


    }

    public void SetToHotBar(ScriptOBJ item, bool status)
    {


        HotBar.GetComponent<UIHotBarCtrl>().setItemToSlot(item, status);



    }
    public void modifyItem(string nameItem)
    {
        for (int i = items.Count - 1; i >= 0; i--)
        {
            ScriptOBJ item = items[i];
            if (item.nameItem == nameItem)
            {
                player.ItemUse = item;
                player.isUse = true;
                if (item.amount == 1)
                {
                    items.RemoveAt(i);
                }
                else
                {
                    item.amount -= 1;
                }
            }
        }
    }




}
