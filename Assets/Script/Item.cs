using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public virtual void AddToHotBar(int index)
    {
        Debug.Log("Add: " + name + "to " + index);
    }

}
