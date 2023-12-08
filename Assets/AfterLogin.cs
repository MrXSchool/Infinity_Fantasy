using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Newtonsoft.Json;

public class AfterLogin : MonoBehaviour
{

    public GameObject Menu_panel_after_login;
    public GameObject setting_panel;
    public GameObject Load_panel;
    public GameObject saveFIle;
    public Button newgame, loadgame, setting, exit;
    LoadingScript loadingScript;
    public GameObject saveSlot;
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
        saveSlot = Resources.Load<GameObject>("saveSlot");


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void newGame()
    {
        gameObject.SetActive(false);
        SceneLoader sceneLoader = GameObject.Find("menu").GetComponent<SceneLoader>();
        sceneLoader.isload = false;
        loadingScript.LoadLevel(1);

    }


    public void settingGame()
    {
        gameObject.SetActive(false);
        setting_panel.SetActive(true);
    }


    public void LoadGame()
    {
        try
        {
            PlayerPrefs.GetString("username");
            SceneLoader sceneLoader = GameObject.Find("menu").GetComponent<SceneLoader>();
            sceneLoader.isload = true;
            Menu_panel_after_login.SetActive(false);
            Load_panel.SetActive(true);
            string file = System.IO.File.ReadAllText(Application.dataPath + "/Data/user/" + PlayerPrefs.GetString("username") + ".json");
            User user = JsonConvert.DeserializeObject<User>(file);
            List<MapModel> mapModels = user.data;
            int Count = 0;
            foreach (MapModel map in mapModels)
            {
                GameObject slot = Instantiate(saveSlot);
                slot.transform.SetParent(saveFIle.transform);
                slot.name = "slot" + Count.ToString();
                slot.GetComponentInChildren<TMP_Text>().text = map.sceneName;
                if (map.player.playerAvatar != null)
                {
                    slot.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>(map.player.playerAvatar);
                }
                else
                {
                    slot.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Assets/Resource/Player/Samurai/Samurai_Commander/Idle.png");
                }
                slot.GetComponentsInChildren<TMP_Text>()[2].text = map.player.hp.ToString();
                slot.GetComponentsInChildren<TMP_Text>()[3].text = map.player.mana.ToString();
                slot.GetComponentsInChildren<TMP_Text>()[5].text = "X: " + System.Math.Round(map.player.position[0], 2).ToString() + " Y: " + System.Math.Round(map.player.position[1], 2).ToString();
                Count++;
            }

        }
        catch (System.Exception)
        {

            throw;
        }



        // gameObject.SetActive(false);
        // SaveFile_panel.SetActive(true);
    }


    public void loadLevel(string sceneName)
    {
        loadingScript.LoadLevel(getLevel(sceneName));
    }

    public int getLevel(string sceneName)
    {
        //intro 0 map1 1 map2 2 map3 3 
        if (sceneName == "intro")
        {
            return 0;
        }
        else if (sceneName == "map1")
        {
            return 1;
        }
        else if (sceneName == "map2")
        {
            return 2;
        }
        else if (sceneName == "map3")
        {
            return 3;
        }
        else if (sceneName == "sceneTest_X")
        {
            return 4;
        }
        else
        {
            return 0;
        }

    }


}
