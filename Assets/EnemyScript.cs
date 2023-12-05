using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public float start, end;
    public float skillrange;
    public float attackrange;
    public float speed = 3;
    public float hp = 100;
    public float damage = 10;
    public bool isSkill;
    public bool isAttack;
    public bool isWalk;
    public bool isDead;
    public bool isRight;
    public bool hasTargetFar;
    public bool hasTargetNear;
    public GameObject target;
    public float distance;
    public Animator ani;
    public Interactable interactable;
    public string currentState;
    public string nameEnemy;
    public float skillCD = 5f;




    void Start()
    {

        isSkill = false;
        isAttack = false;
        isDead = false;
        isRight = true;
        hasTargetFar = false;
        hasTargetNear = false;
        if (start == 0 && end == 0)
        {
            start = transform.position.x - 5;
            end = transform.position.x + 5;
        }
        nameEnemy = gameObject.name;
        ani = GetComponent<Animator>();
        interactable = GetComponent<Interactable>();
        skillrange = interactable.radius1;
        attackrange = interactable.radius;

    }


    void Update()
    {
        // //thời gioan chờ skill
        // if (!isSkill)
        // {
        //     skillCD -= Time.deltaTime;
        //     if (skillCD <= 0f)
        //     {
        //         isSkill = true;
        //         skillCD = 5f;

        //     }
        // }
        target = GameObject.FindGameObjectWithTag("Player");

        //di chuyen khi khong tan cong
        // if (!hasTargetFar)
        // {
        //     Move();
        //     limitMove();

        // }

        //tính khoảng cách từ bản thân đến người chơi
        distance = Vector3.Distance(transform.position, target.transform.position);

        //kiem tra xem co nguoi choi trong tam nhin hay khong
        if (distance <= skillrange)
        {
            hasTargetFar = true;
        }
        else
        {
            hasTargetFar = false;
        }

        //kiem tra xem co nguoi choi trong tam tan cong hay khong
        if (distance <= attackrange)
        {
            hasTargetNear = true;
        }
        else
        {
            hasTargetNear = false;
        }

        //kiem tra xem co nguoi choi trong tam nhin hay khong
        if (hasTargetFar)
        {
            isAttack = false;
        }

        //kiem tra xem co nguoi choi trong tam tan cong hay khong
        if (hasTargetNear)
        {
            isAttack = true;
            lookAtTarget();
            changeAnimation(nameEnemy + "_attack");
        }

        //kiem tra xem co nguoi choi trong tam nhin hay khong
        if (!hasTargetFar && !hasTargetNear)
        {
            if (!isWalk)
            {
                changeAnimation(nameEnemy + "_idle");
            }
        }




    }


    //quay mat ve phia nguoi choiw khi tan cong
    public void lookAtTarget()
    {
        if (target.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }




    //quay matj khi den gioi han
    public void limitMove()
    {
        if (transform.position.x >= end)
        {
            isRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (transform.position.x <= start)
        {
            isRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }



    //di chuyen
    public void Move()
    {
        changeAnimation(nameEnemy + "_walk");
        isWalk = true;
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }


    //chuyen animation
    public void changeAnimation(string newState)
    {

        if (currentState == newState) return;

        ani.Play(newState);

        currentState = newState;
    }


}
