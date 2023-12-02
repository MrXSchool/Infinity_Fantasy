using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equiqment", menuName = "Inventory/Equiqment")]
public class Equiqment : Item
{
    public EqiqmentSlot equipSlot;
    public float damageModifier;
    public float armorModifier;
    public float jMaxModifier;

    public override void Use()
    {
        base.Use();
        EquiqmentManager.instance.Equiq(this);
    }
}

public enum EqiqmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }

