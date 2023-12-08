using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderShotgunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyScript enemy;
    void Start()
    {
        enemy.GetComponent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.hasTargetFar && !enemy.isSkill && !enemy.isAttack && !enemy.isDead)
        {
            enemy.moveEnable = false;
            //enemy.isSkill = true;
            enemy.changeAnimation(enemy.nameEnemy + "_skill1");
        }
    }

    public void reCharge() {
        enemy.changeAnimation(enemy.nameEnemy + "_recharge");
        enemy.isSkill = true;
        Debug.Log("recharge");
    }
}
