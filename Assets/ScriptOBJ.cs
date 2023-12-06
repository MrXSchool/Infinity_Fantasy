using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ScriptOBJ : ScriptableObject
{
    public float hpRestore;
    public float manaRestore;
    public float hpAdd;
    public float manaAdd;
    public int jumMaxAdd;
    public float damageAdd;

    public Sprite image;
    public string nameItem;
    public string description;
}
