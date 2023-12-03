using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;
    Enemy enemy;
    Animator animator;

    private void Awake()
    {
        col = GameObject.Find("eHitBox").GetComponent<Collider2D>();
        enemy = GetComponentInParent<Enemy>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            detectedColliders.Add(collision);
        }

        if (collision.gameObject.tag == "pHitBox")
        {
            enemy.GetComponent<Damageable>().Hit(10);


        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            detectedColliders.Remove(collision);
        }

    }




}
