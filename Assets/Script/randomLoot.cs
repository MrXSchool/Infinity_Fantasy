using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class randomLoot : MonoBehaviour
{
    public GameObject dropitemPref;
    public List<ScriptableObject> lootList = new List<ScriptableObject>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ScriptOBJ getDroppedItem()
    {
        int randomNum = Random.Range(1, 101);
        List<ScriptOBJ> possibleItems = new List<ScriptOBJ>();
        foreach (ScriptOBJ item in lootList)
        {

            if (randomNum <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            ScriptOBJ droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;

    }

    public void InstantiateLoot(Vector3 spwanPos)
    {
        ScriptOBJ loot = getDroppedItem();
        if (loot != null)
        {
            GameObject lootgameobj = Instantiate(dropitemPref, spwanPos, Quaternion.identity);
            lootgameobj.GetComponent<ItemPickUp>().item = loot;

            float dropForce = 5f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootgameobj.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
        }

    }
}
