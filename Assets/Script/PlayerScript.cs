using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    private Animator ani;
    private Rigidbody2D rb;
    [SerializeField]
    private float walkSpeed = 5f;
    private float runSpeed = 10f;
    private float jumpForce = 5f;
    [SerializeField]
    private String currentState;
    private float Horizontal;
    [SerializeField]
    private bool isGround = true;

    [SerializeField]
    private float Jcount = 0, JcountMax = 2;
    public float hp = 100, mana = 100;

    private bool isAttack = false;
    private bool isRun = false;
    private bool isProtect = false;

    public GameObject healBar;
    public GameObject manaBar;




    //trang thanh nhan vat
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_WALK = "Walk";
    const string PLAYER_ATTACK_1 = "Attack_1";
    const string PLAYER_ATTACK_2 = "Attack_2";
    const string PLAYER_ATTACK_3 = "Attack_3";
    const string PLAYER_JUMP = "Jump";
    const string PLAYER_PROTECT = "Protect";
    const string PLAYER_HURT = "Hurt";
    const string PLAYER_DEAD = "Dead";

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame

    void Update()
    {

        //cập nhật thanh máu
        healBar.GetComponent<HealBarScript>().SetHeal(hp);
        manaBar.GetComponent<HealBarScript>().SetMana(mana);


        //DI CHUYỂN THEO TỐC ĐỘ ĐI HOẶC CHẠY
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (!isRun)
        {
            rb.velocity = new Vector2(Horizontal * walkSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Horizontal * runSpeed, rb.velocity.y);
        }


        //QUAY TRÁI PHẢI
        if (Input.GetKeyDown(KeyCode.D))
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //NHẢY
        if (Input.GetKeyDown(KeyCode.Space) && Jcount <= JcountMax)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGround = false;
            changeAnimation(PLAYER_JUMP);
            Jcount += 1;
        }


        // TẤN CÔNG COMBO
        if (Input.GetKeyDown(KeyCode.J))
        {

            if (currentState == PLAYER_IDLE || currentState == PLAYER_WALK)
            {
                ani.SetInteger("attack", 1);
                changeAnimation(PLAYER_ATTACK_1);
                Debug.Log("1");
                isAttack = true;
            }
            else if (currentState == PLAYER_ATTACK_1)
            {
                ani.SetInteger("attack", 2);
                changeAnimation(PLAYER_ATTACK_2);
                Debug.Log("2");

            }
            else if (currentState == PLAYER_ATTACK_2)
            {
                ani.SetInteger("attack", 3);
                changeAnimation(PLAYER_ATTACK_3);
                Debug.Log("3");


            }

        }
        // Nhận AnimatorClipInfo hiện tại (chứa thông tin về ANIMATION hiện tại)
        AnimatorClipInfo[] clipInfo = ani.GetCurrentAnimatorClipInfo(0);

        // LẤY TÊN CỦA ANIMATION HIỆN TẠI
        string currentAnimationName = clipInfo[0].clip.name;



        if (currentAnimationName == PLAYER_IDLE)
        {
            isAttack = false;
        }



        //ĐỠ
        if (Input.GetKey(KeyCode.LeftControl) && !isProtect)
        {
            changeAnimation(PLAYER_PROTECT);
            isProtect = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && isProtect)
        {
            isProtect = false;
        }


        // NGHỈ , ĐI VÀ CHẠY
        if (isGround && !isAttack)
        {

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isRun = false;
                ani.SetBool("isRun", false);
            }

            if (Input.GetKey(KeyCode.LeftShift) && Horizontal != 0)
            {
                changeAnimation(PLAYER_RUN);
                isRun = true;
                ani.SetBool("isRun", true);
            }
            else if (Horizontal != 0 && (isRun == false || currentAnimationName == PLAYER_IDLE))
            {
                changeAnimation(PLAYER_WALK);

                isRun = false;
                ani.SetBool("isRun", false);
            }
            else
            {
                changeAnimation(PLAYER_IDLE);
            }
        }
    }

    void changeAnimation(String newState)
    {

        if (currentState == newState) return;

        ani.Play(newState);

        currentState = newState;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isGround = true;
            Debug.Log(other.gameObject.name);
            Jcount = 0;
        }
    }

}
