using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;


public class UIHotBarCtrl : MonoBehaviour
{
    private static UIHotBarCtrl instance;

    public static UIHotBarCtrl Instance { get => instance; }
    public GameObject player;


    protected void Awake()
    {
        // if (UIHotBarCtrl.instance != null)
        UIHotBarCtrl.instance = this;
    }


    private void Update()
    {
        if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
        getHotKey();
    }

    public void getHotKey()
    {
        Dictionary<KeyCode, int> keyToIndex = new Dictionary<KeyCode, int>
    {
        {KeyCode.Alpha1, 0},
        {KeyCode.Alpha2, 1},
        {KeyCode.Alpha3, 2},
        {KeyCode.Alpha4, 3},
        {KeyCode.Alpha5, 4},
        {KeyCode.Alpha6, 5},
        {KeyCode.Alpha7, 6}
    };

        foreach (var keyIndexPair in keyToIndex)
        {
            KeyCode key = keyIndexPair.Key;
            int index = keyIndexPair.Value;

            if (Input.GetKeyDown(key))
            {
                if (getItemSlotByIndex(index).GetComponent<ItemSlot>().transform.childCount == 0) return;
                getItemSlotByIndex(index).GetComponent<ItemSlot>().changeValue(1, "-");
                player.GetComponent<Inventory>().modifyItem(getItemSlotByIndex(index).GetComponent<ItemSlot>().getCurrentItem().name);
                break; // Khi đã xử lý một key, thoát khỏi vòng lặp
            }
        }
    }


    public GameObject getItemSlotByIndex(int index)
    {
        GameObject ItemSlot = transform.GetChild(0).GetChild(0).GetChild(index).gameObject;
        return ItemSlot;
    }


    public void setItemToSlot(ScriptOBJ item, bool isUpdate)
    {

        // for (int i = 0; i < 5; i++)
        // {
        //     if (getItemSlotByIndex(i).transform.childCount == 0 && !isUpdate)
        //     {
        //         Debug.Log(item.nameItem);
        //         getItemSlotByIndex(i).GetComponent<ItemSlot>().addNewItem(item);
        //         return;

        //     }
        //     else
        //     {
        //         Debug.Log("childCount: " + getItemSlotByIndex(i).transform.childCount);
        //         Debug.Log(item.name);
        //         if (getItemSlotByIndex(i).GetComponent<ItemSlot>().getCurrentItem().name == item.nameItem)
        //         {
        //             getItemSlotByIndex(i).GetComponent<ItemSlot>().changeValue(item.amount, "+");
        //         }
        //     }
        // }

        if (!isUpdate)
        {
            // Thêm mới nếu có ô trống
            for (int i = 0; i < 5; i++)
            {
                if (getItemSlotByIndex(i).transform.childCount == 0)
                {
                    getItemSlotByIndex(i).GetComponent<ItemSlot>().addNewItem(item);
                    return;
                }
            }
        }
        else
        {
            // Kiểm tra trùng lặp và thay đổi giá trị nếu cần
            for (int i = 0; i < 5; i++)
            {
                ItemSlot itemSlot = getItemSlotByIndex(i).GetComponent<ItemSlot>();

                if (itemSlot.transform.childCount > 0 && itemSlot.getCurrentItem().name == item.nameItem)
                {
                    itemSlot.changeValue(item.amount, "+");
                    return;
                }
            }
        }

    }




}
