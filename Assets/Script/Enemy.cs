using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{

    public float start, end;
    public float speed;
    private bool isRight;
    public DetectionZone attackZone;
    Animator animator;
    Collider2D cd;
    Rigidbody2D rb;

    public GameObject player;

    public bool _hasTarget = false;
    public bool HasTarget { get { return _hasTarget; } private set 
        { 
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        } 
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cd = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Di chuyển
        var positionEnemy = transform.position.x;

        // Dí theo player
        var positionPlayer = player.transform.position.x;
        if(positionPlayer > start && positionPlayer < end)
        {
            if(positionPlayer < positionEnemy) isRight = false;
            if(positionPlayer > positionEnemy) isRight = true;
        }



        if(positionEnemy < start)
        {
            isRight = true;
        }

        if(positionEnemy > end)
        {
            isRight = false;
        }

        Vector2 scale = transform.localScale;
        
        if(CanMove)
        {
            if (isRight)
            {
                scale.x = 1;
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else
            {
                scale.x = -1;
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(Vector3.left * 0);
        }
        

        transform.localScale = scale;

        HasTarget = attackZone.detectedColliders.Count > 0;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "touching" ) 
        {
            isRight = !isRight;
        }

        if ( collision.gameObject.tag == "player")
        {
            
            rb.bodyType = RigidbodyType2D.Static;

        }
        else
        {
            
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }



}
