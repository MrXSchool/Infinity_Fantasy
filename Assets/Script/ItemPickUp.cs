
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pickUp();
        }
    }
    void pickUp()
    {
        Debug.Log("Picking up " + item.name);
        Inventory.instance.Add(item);
        Destroy(gameObject);
    }
}
