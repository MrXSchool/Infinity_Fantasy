using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiqmentManager : MonoBehaviour
{
    public static EquiqmentManager instance;
    public delegate void OnEquiqmentChanged(Equiqment newItem, Equiqment oldItem);
    public OnEquiqmentChanged onEquiqmentChanged;

    void Awake()
    {
        instance = this;
    }

    Equiqment[] currentEquiqment;



    void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EqiqmentSlot)).Length;
        currentEquiqment = new Equiqment[numSlots];

    }

    public void Equiq(Equiqment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        currentEquiqment[slotIndex] = newItem;
        if (onEquiqmentChanged != null)
        {
            onEquiqmentChanged.Invoke(newItem, null);
        }
    }
}
