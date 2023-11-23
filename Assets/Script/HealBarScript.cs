using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHeal(float heal)
    {
        slider.maxValue = heal;
        slider.value = heal;
    }

    public void SetHeal(float heal)
    {
        slider.value = heal;
    }

    public void SetMaxMana(float mana)
    {
        slider.maxValue = mana;
        slider.value = mana;
    }

    public void SetMana(float mana)
    {
        slider.value = mana;
    }

}
