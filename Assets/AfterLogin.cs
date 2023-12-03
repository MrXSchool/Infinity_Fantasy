using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterLogin : MonoBehaviour
{

    public GameObject Menu_panel_after_login;
    public GameObject setting_panel;
    public Button newgame, loadgame, setting, exit;
    LoadingScript loadingScript;
    // Start is called before the first frame update
    void Start()
    {
        setting_panel = GameObject.Find("setting");
        Menu_panel_after_login = GameObject.Find("Menu_panel_after_login");
        newgame = Menu_panel_after_login.GetComponentsInChildren<Button>()[0];
        loadgame = Menu_panel_after_login.GetComponentsInChildren<Button>()[1];
        setting = Menu_panel_after_login.GetComponentsInChildren<Button>()[2];
        exit = Menu_panel_after_login.GetComponentsInChildren<Button>()[3];
        loadingScript = GameObject.Find("Loading").GetComponent<LoadingScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void newGame()
    {
        loadingScript.LoadLevel(1);
    }


    public void settingGame()
    {
        gameObject.SetActive(false);
        setting_panel.SetActive(true);
    }






}
