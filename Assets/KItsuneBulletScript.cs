using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KItsuneBulletScript : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    public float damage = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerScript>().takeDamage(damage);
            Destroy(gameObject);
            Instantiate(Resources.Load("Fire_1_hit"), other.transform.position, Quaternion.identity);

        }
    }
}
