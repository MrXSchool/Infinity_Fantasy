using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ScriptOBJ : ScriptableObject
{
    public enum TypeOfItem
    {
        Restore,
        AddMax,
    }

    public TypeOfItem typeOfItem;
    public int amount;
    public int maxStack;
    public float hpRestore;
    public float manaRestore;
    public float hpAdd;
    public float manaAdd;
    public int jumMaxAdd;
    public float damageAdd;

    public Sprite image;
    public string nameItem;
    public string description;


    public virtual void Use()
    {
        Debug.Log("useritem");
    }

    public virtual void addToItemSlot()
    {
        Debug.Log("addToItemSlot");
    }

    public ScriptOBJ Clone()
    {
        ScriptOBJ clonedObject = CreateInstance<ScriptOBJ>();
        clonedObject.typeOfItem = this.typeOfItem;
        clonedObject.amount = this.amount;
        clonedObject.maxStack = this.maxStack;
        clonedObject.hpRestore = this.hpRestore;
        clonedObject.manaRestore = this.manaRestore;
        clonedObject.hpAdd = this.hpAdd;
        clonedObject.manaAdd = this.manaAdd;
        clonedObject.jumMaxAdd = this.jumMaxAdd;
        clonedObject.damageAdd = this.damageAdd;
        clonedObject.image = this.image;
        clonedObject.nameItem = this.nameItem;
        clonedObject.description = this.description;
        return clonedObject;
    }
}
