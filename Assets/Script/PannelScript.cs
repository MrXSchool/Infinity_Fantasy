using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //

public class PannelScript : MonoBehaviour
{
    public AudioClip clip;
    public GameObject menu;
    public bool isActived = false;
    //volume
    [Range(0, 1)]
    public float volume;
    public GameObject seting;
    public GameObject load;
    public GameObject save;
    public GameObject exit;

    public GameObject allMusic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            isActived = !isActived;
            if (isActived)
            {
                menu.SetActive(true);
                Time.timeScale = 0;
                allMusic.GetComponentInChildren<AudioSource>().Pause();
            }
            else
            {
                menu.SetActive(false);
                seting.SetActive(false);
                // load.SetActive(false);
                // save.SetActive(false);
                // exit.SetActive(false);
                Time.timeScale = 1;
                allMusic.GetComponentInChildren<AudioSource>().Play();

            }
        }
    }

    public void resumeClick()
    {
        isActived = false;
        menu.SetActive(false);
        Time.timeScale = 1;
        allMusic.GetComponentInChildren<AudioSource>().Play();
    }

    public void setingClick()
    {
        seting.SetActive(true);
        menu.SetActive(false);
    }

    public void loadClick()
    {
        load.SetActive(true);
        menu.SetActive(false);
    }

    public void saveClick()
    {
        save.SetActive(true);
        menu.SetActive(false);
    }

    public void exitClick()
    {
        exit.SetActive(true);
        menu.SetActive(false);
    }

    public void exitOke()
    {
        //chuyển về màn hình chính
        SceneManager.LoadScene("intro");
        Debug.Log("oke");
    }

    public void backClick()
    {
        //check true false
        if (seting.activeSelf)
        {
            seting.SetActive(false);
            menu.SetActive(true);
        }
        else if (load.activeSelf)
        {
            load.SetActive(false);
            menu.SetActive(true);
        }
        else if (save.activeSelf)
        {
            save.SetActive(false);
            menu.SetActive(true);
        }
        else if (exit.activeSelf)
        {
            exit.SetActive(false);
            menu.SetActive(true);
        }
    }


}
