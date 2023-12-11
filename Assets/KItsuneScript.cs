using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KItsuneScript : MonoBehaviour
{
    public Transform player;
    public EnemyScript ene;
    public Interactable interactable;
    public GameObject skillBall;
    public Animator ani;
    public GameObject firePos;
    public bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ene = gameObject.GetComponent<EnemyScript>();
        interactable = gameObject.GetComponent<Interactable>();
        ani = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // firePos = GameObject.Find("firePos");


        if (ene.hasTargetFar && !ene.isSkill && !ene.hasTargetNear)
        {
            ene.moveEnable = false;
            Debug.Log("33");
            ene.changeAnimation(ene.nameEnemy + "_skill1");
            Debug.Log("banggg!!");
        }
        else
        {


            if (!ene.hasTargetNear)
            {
                ene.moveEnable = true;
            }
        }



    }

    public void shoot()
    {
        ene.isSkill = true;
        Instantiate(Resources.Load("Fire_1"), firePos.transform.position, firePos.transform.rotation);
    }


}

