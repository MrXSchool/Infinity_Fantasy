using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{


    public void OnDrop(PointerEventData eventData)
    {

        if (transform.childCount > 0) return;

        GameObject dropObj = eventData.pointerDrag;
        DragItem dragItem = dropObj.GetComponent<DragItem>();
        dragItem.realParent = transform;

    }

    public GameObject getCurrentItem()
    {

        GameObject item = transform.GetChild(0).gameObject;
        return item;
    }

    public void changeItem(GameObject newItem)
    {

        Transform childTransform = transform.GetChild(0);

        // Đặt newItem là con của cùng cha với childTransform
        newItem.transform.SetParent(childTransform.parent);

        // Đặt vị trí và quay góc của newItem giống với childTransform
        newItem.transform.position = childTransform.position;
        newItem.transform.rotation = childTransform.rotation;

        // Hủy kết nối cha-con giữa childTransform và parent (cũ)
        childTransform.SetParent(null);

        // Hủy đối tượng cũ (nếu cần)
        Destroy(childTransform.gameObject);

    }

    public void addNewItem(ScriptOBJ obj)
    {
        GameObject item = Instantiate(Resources.Load("hotbar/item")) as GameObject;
        item.transform.SetParent(transform);
        item.transform.localScale = transform.localScale;
        item.name = obj.nameItem;
        // item.transform.position = transform.position;
        // item.transform.rotation = transform.rotation;
        item.GetComponent<Image>().sprite = obj.image;
        item.GetComponentInChildren<TMP_Text>().text = obj.amount.ToString();

    }

    public void changeValue(int amount, string s)
    {
        int currentAmount = Int32.Parse(transform.GetChild(0).GetComponentInChildren<TMP_Text>().text);

        if (s == "+")
        {
            int newAmount = currentAmount + amount;
            transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = newAmount.ToString();
        }
        else if (s == "-")
        {
            int newAmount = currentAmount - amount;
            if (newAmount <= 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = newAmount.ToString();

        }
        else if (s == "modify")
        {
            transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = amount.ToString();
        }

    }


}
