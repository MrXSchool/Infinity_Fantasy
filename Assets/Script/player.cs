using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float speed = 8f;
    public bool isRight;
    private Animator anim;

    public int jumpCount = 0;
    public bool isGrounded;
    public float jumpF = 5f;
    public GameObject panel;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scale = transform.localScale;
        

        if (isRight)
        {
            scale.x = 1;
        }
        else
        {
            scale.x = -1;
        }

        // Move
        /*
         * GetKey
         * GetKeyDown
         * GetKeyUp
         */

        transform.localScale = scale;

        if (Input.GetKey(KeyCode.RightArrow))
        {
             Vector3 vector3 = new Vector3(1, 0, 0);
             transform.Translate(Vector3.right * Time.deltaTime * speed);

            
           // rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x*speed, rigidbody2D.velocity.y);
            isRight = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
             Vector3 vector3 = new Vector3(1, 0, 0);
             transform.Translate(Vector3.left * Time.deltaTime * speed);

            
            //rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x * (- speed), rigidbody2D.velocity.y);
            isRight = false;
        }




        if (Input.GetKeyDown(KeyCode.UpArrow) && doubleJump())
        {
            rigidbody2D.AddForce(new Vector2(0, jumpF), ForceMode2D.Impulse);
            jumpCount++;
            if (jumpCount == 2)
            {
                isGrounded = false;
            }
        }
    }

    public bool doubleJump()
    {
        if (isGrounded == true && jumpCount < 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }


    }

   

}
