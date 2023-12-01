using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcheckwall : MonoBehaviour
{
    public PlayerScript player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("in");
            player.Wall = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log("out");
            player.Wall = false;
        }
    }
}
