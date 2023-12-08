using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[10];
    public GameObject menu;

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
    public bool isGround = true;

    [SerializeField]
    public float Jcount = 0, JcountMax = 2;
    public float hp = 100, mana = 100;
    public string playerAvatar;
    public string playerName;

    public bool isAttack = false;
    public bool isRun = false;
    public bool isProtect = false;
    public bool isDead = false;
    public bool isWalk = false;

    public GameObject healBar;
    public GameObject manaBar;
    public GameObject checkWall;
    public bool Wall = false;
    public int comboCount;




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
        healBar = GameObject.Find("HealBar");
        manaBar = GameObject.Find("ManaBar");
        Sprite sprite = GetComponentInParent<SpriteRenderer>().sprite;
        playerAvatar = AssetDatabase.GetAssetPath(sprite.texture);
        playerName = this.name;
        menu = GameObject.Find("menu");
    }


    public void loadPlayer()
    {
        PlayerModel player = RWJson.LoadPlayer();
        this.hp = player.hp;
        this.mana = player.mana;
        this.transform.position = new Vector3(player.position[0], player.position[1], player.position[2]);
    }

    // Update is called once per frame

    void Update()
    {

        if (hp <= 0)
        {
            playerDead();
        }

        if (transform.position.y <= -29 && menu.GetComponent<PannelScript>().currentScene == "map1")
        {
            hp = 0;
        }



        // isWall();
        //cập nhật thanh máu
        healBar.GetComponent<HealBarScript>().SetHeal(hp);
        manaBar.GetComponent<HealBarScript>().SetMana(mana);


        //DI CHUYỂN THEO TỐC ĐỘ ĐI HOẶC CHẠY
        Horizontal = Input.GetAxisRaw("Horizontal");
        if (!Wall)
        {

            if (!isRun)
            {
                rb.velocity = new Vector2(Horizontal * walkSpeed, rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(Horizontal * runSpeed, rb.velocity.y);
            }
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
        // if (Input.GetKeyDown(KeyCode.J))
        // {

        //     if (currentState == PLAYER_IDLE || currentState == PLAYER_WALK)
        //     {
        //         if (!isAttack)
        //         {
        //             isAttack = true;
        //             comboCount++;
        //             // Kiểm tra và thực hiện animation tương ứng
        //             if (comboCount == 1)
        //                 changeAnimation(PLAYER_ATTACK_1);
        //             else if (comboCount == 2)
        //                 changeAnimation(PLAYER_ATTACK_2);
        //             else if (comboCount == 3)
        //                 changeAnimation(PLAYER_ATTACK_3);
        //             // Các combo tiếp theo có thể được thêm vào tại đây

        //             // Reset combo sau khi hoàn thành
        //             if (comboCount >= 3 && !isAttack)
        //                 comboCount = 0;
        //         }
        //     }

        // }
        // Nhận AnimatorClipInfo hiện tại (chứa thông tin về ANIMATION hiện tại)
        AnimatorClipInfo[] clipInfo = ani.GetCurrentAnimatorClipInfo(0);

        // LẤY TÊN CỦA ANIMATION HIỆN TẠI
        string currentAnimationName = clipInfo[0].clip.name;


        if (!isAttack && !isRun && !isWalk)
        {
            changeAnimation(PLAYER_IDLE);
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
        if (isGround && !isAttack && hp > 0)
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
            else if (Horizontal != 0 && (!isRun || currentAnimationName == PLAYER_IDLE))
            {
                changeAnimation(PLAYER_WALK);
                isWalk = true;
                isRun = false;
                ani.SetBool("isRun", false);
            }
            else
            {
                isWalk = false;
                changeAnimation(PLAYER_IDLE);
            }
        }

        if (currentAnimationName != PLAYER_ATTACK_1 || currentAnimationName != PLAYER_ATTACK_2 || currentAnimationName != PLAYER_ATTACK_3)
        {
            isAttack = false;
        }

        if (!isAttack && !isWalk)
        {
            return;
        }
        else
        {
            isWalk = !isAttack;
        }


    }

    void changeAnimation(String newState)
    {

        if (currentState == newState) return;

        ani.Play(newState);

        currentState = newState;
    }




    // private void isWall()
    // {
    //     var start = new Vector2(checkWall.transform.position.x, checkWall.transform.position.y);
    //     RaycastHit2D hit = Physics2D.Raycast(start, Vector2.right, 0.07f);
    //     Debug.DrawRay(start, Vector2.right * 0.07f, Color.red);
    //     if (hit.collider != null)
    //     {
    //         Wall = true;
    //         Debug.Log(hit.collider.name);
    //     }
    //     else
    //     {
    //         Wall = false;
    //     }







    // }

    public void playerStatusBonus(float hp, float mana, float damage)
    {
        this.hp += hp;
        this.mana += mana;

    }
    public void playerDead()
    {
        changeAnimation(PLAYER_DEAD);
        isDead = true;
        //disable all component of player
        this.enabled = false;


    }

    public void takeDamage(float damage)
    {
        if (!isProtect)
        {
            hp -= damage;

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "item")
        {
            Debug.Log("item");
            inventory[inventory.Length + 1] = other.gameObject;
            playerStatusBonus(other.gameObject.GetComponent<itemScript>().BonusHP, other.gameObject.GetComponent<itemScript>().BonusMana, other.gameObject.GetComponent<itemScript>().BonusDamage);
            Destroy(other.gameObject);
        }


    }
}
