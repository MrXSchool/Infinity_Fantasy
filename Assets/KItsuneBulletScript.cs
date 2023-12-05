using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KItsuneBulletScript : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().takeDamage(10);
            Destroy(gameObject);
            GameObject hit = Instantiate(Resources.Load("Fire_1_hit")) as GameObject;
            Destroy(hit, 1f);
        }
    }
}
