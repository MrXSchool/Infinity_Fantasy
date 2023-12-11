using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eHealBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public EnemyScript enemy;
    void Start()
    {
        slider = GetComponent<Slider>();
        enemy = transform.parent.parent.GetComponent<EnemyScript>();
        slider.maxValue = enemy.hp;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = enemy.hp;
        transform.position = new Vector3(transform.parent.parent.transform.position.x, transform.parent.parent.transform.position.y + 1.5f, transform.parent.parent.transform.position.z);

    }
}
