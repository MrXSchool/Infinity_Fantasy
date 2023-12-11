using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealBarScript : MonoBehaviour
{
    public Slider slider;
    public TMP_Text maxHP;
    public TMP_Text maxMana;
    public TMP_Text currentHP;
    public TMP_Text currentMana;


    public void SetMaxHeal(float heal)
    {
        slider.maxValue = heal;
        slider.value = heal;
        maxHP.text = heal.ToString();
    }

    public void SetHeal(float heal)
    {
        slider.value = heal;
        currentHP.text = heal.ToString();
    }

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
        maxMana.text = mana.ToString();
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
        currentMana.text = mana.ToString();
    }



}
