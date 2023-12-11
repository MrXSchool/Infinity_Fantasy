using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderShotgunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyScript enemy;
    public Interactable interactable;
    void Start()
    {
        enemy = GetComponent<EnemyScript>();
        interactable =GetComponent<Interactable>();
        enemy.skillCD=3.9f;
    }

    // Update is called once per frame
    void Update()
    { 

        //phát hiện player trong vùng skill và skill = false thì skill
        if (enemy.hasTargetFar && !enemy.isSkill && !enemy.isAttack && !enemy.isDead)
        {
            //khi dùng skill thì moveEnable = false kích họat animation skill và skill = true
            enemy.moveEnable = false;
            //enemy.isSkill = true;
            enemy.changeAnimation(enemy.nameEnemy + "_skill1");
            

        }
        if(enemy.isSkill)
        {
            enemy.moveEnable = false;
        }

        

    }

    public void reCharge() {
        //khi skill = true thì thực hiện animation recharge và moveEnable = false tắt detectedZone
        if (enemy.isSkill)
        {
            enemy.moveEnable = false;
            enemy.changeAnimation(enemy.nameEnemy + "_recharge");
            interactable.detectedZone = 0f;
            Debug.Log("recharge");
        }
    }

    public void finishAni() {
        enemy.isSkill = true;
        
            reCharge();
        
       }

    //khi recharge xong thì skill = false và moveEnable = true bật lại detectedZone
    
    public void finishAni1()
    {
        enemy.moveEnable = true;
        interactable.detectedZone = 5f;
        Debug.Log("recharge Done");
    }


}
