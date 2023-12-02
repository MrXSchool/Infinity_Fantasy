using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    PlayerScript player;
    void Start()
    {
        EquiqmentManager.instance.onEquiqmentChanged += OnEquipmentChanged;
        player = GetComponent<PlayerScript>();


    }

    void OnEquipmentChanged(Equiqment newItem, Equiqment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
            jMax.AddModifier(newItem.jMaxModifier);
            player.JcountMax = jMax.getFinalValue();
        }
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }




}
