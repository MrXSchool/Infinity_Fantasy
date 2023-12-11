
using UnityEngine;
using UnityEngine.UI;

public class ItemPickUp : MonoBehaviour
{
    public SpriteRenderer image;
    public ScriptOBJ item;

    private void Start()
    {
        image = GetComponent<SpriteRenderer>();
        image.sprite = item.image;

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUp();
        }
    }
    void pickUp()
    {
        ScriptOBJ clone = item.Clone();
        Debug.Log("Picking up " + clone.name);
        bool WasPickup = Inventory.instance.Add(clone);
        if (WasPickup)
            Destroy(gameObject);
    }
}
