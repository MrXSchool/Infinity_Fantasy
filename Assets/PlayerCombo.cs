using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{

    public Animator ani;
    public int combo;
    public AudioSource audioSource;
    AudioClip[] audioClips;
    public PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerScript = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Combo();

        if (playerScript.isWalk)
        {
            combo = 0;
        }
    }

    public void startCombo()
    {
        playerScript.isAttack = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void finishAni()
    {
        playerScript.isAttack = false;
        combo = 0;
    }

    public void Combo()
    {
        if (Input.GetKeyDown(KeyCode.J) && !playerScript.isAttack && playerScript.isGround)
        {
            playerScript.isAttack = true;
            ani.SetTrigger("" + combo);
            // audioSource.clip = audioClips[combo];
            // audioSource.Play();

        }
    }
}
