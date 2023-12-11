using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 6f;
    public float lifeTime = 3f;
    public float damage = 60f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        transform.localScale = transform.localScale * -1f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.GetComponent<EnemyScript>().takedame(damage);
            Destroy(gameObject);


        }
    }
}
