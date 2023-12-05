using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KItsuneScript : MonoBehaviour
{
    public Transform player;
    public EnemyScript ene;
    public Interactable interactable;
    public GameObject skillBall;
    public Animator ani;
    public GameObject firePos;
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
        firePos = GameObject.Find("firePos");
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= ene.skillrange && !ene.isSkill)
        {
            // Bắn skill đánh xa
            StartCoroutine(LongRangeAttack());
        }
        else if (distanceToPlayer > ene.attackrange)
        {
            // Di chuyển đến vị trí của người chơi
            transform.position = Vector3.MoveTowards(transform.position, player.position, ene.speed * Time.deltaTime);
            ene.changeAnimation(ene.nameEnemy + "_walk");
            ene.isWalk = true;
        }
        else
        {
            // Đánh đánh gần
            ShortRangeAttack();
        }
    }

    IEnumerator LongRangeAttack()
    {
        ene.isSkill = true;

        // Di chuyển ra xa khỏi người chơi
        Vector3 moveDirection = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + moveDirection * ene.skillrange;


        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            ene.changeAnimation(ene.nameEnemy + "_walk");
            ene.isWalk = true;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, ene.speed * Time.deltaTime);
            yield return null;
        }
        ene.isWalk = false;
        ene.changeAnimation(ene.nameEnemy + "_skill1");


        // Hồi chiêu và di chuyển lại
        yield return new WaitForSeconds(ene.skillCD); // Thời gian hồi chiêu
        ene.isSkill = false;
    }

    public void shoot()
    {
        // Bắn skill đánh xa
        Instantiate(Resources.Load("Fire_1"), firePos.transform.position, Quaternion.identity);
    }

    void ShortRangeAttack()
    {
        // Thực hiện đánh gần
        // ...
        ene.changeAnimation(ene.nameEnemy + "_attack");
    }
}

