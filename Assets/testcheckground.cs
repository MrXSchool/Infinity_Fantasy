using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcheckground : MonoBehaviour
{
    public PlayerScript player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            player.isGround = true;

            player.Jcount = 0;
        }

    }

}
