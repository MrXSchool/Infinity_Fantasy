using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    LoadGame loadGame;
    public GameObject Menu_panel;
    public GameObject Menu_panel_after_login;
    public GameObject seting;
    public GameObject Load_panel;
    public GameObject dialog;
    public GameObject Login_panel;
    public GameObject Register_panel;
    public bool isload = false;

    void Start()
    {
        // Đăng ký sự kiện sceneLoaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Hàm được gọi khi Scene được load xong
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        Debug.Log("OnSceneLoaded: " + scene.name);
        loadGame = gameObject.GetComponent<LoadGame>();
        if (!isload) return;
        string[] files = System.IO.Directory.GetFiles("Assets/Data/test", "*.json");
        if (files.Length == 0)
        {
            Debug.Log("No files found.");
        }
        else
        {
            foreach (string file in files)
            {
                Debug.Log(file);
                string json = System.IO.File.ReadAllText(file);
                MapModel mapdata = JsonUtility.FromJson<MapModel>(json);
                if (mapdata.sceneName == scene.name)
                {
                    // string[] name = file.Split('\\');
                    // string[] name1 = name[1].Split('.');
                    loadGame.loadFromJson(mapdata.sceneName);
                }
            }
        }
        // Menu_panel = GameObject.Find("Menu_panel");
        // Menu_panel_after_login = GameObject.Find("Menu_panel_after_login");
        // seting = GameObject.Find("setting");
        // Load_panel = GameObject.Find("Load_panel");
        // dialog = GameObject.Find("dialog");
        // Login_panel = GameObject.Find("Login_panel");
        // Register_panel = GameObject.Find("Register_panel");
        Menu_panel.SetActive(false);
        Menu_panel_after_login.SetActive(false);
        seting.SetActive(false);
        Load_panel.SetActive(false);
        dialog.SetActive(false);
        Login_panel.SetActive(false);
        Register_panel.SetActive(false);


    }

    // Hủy đăng ký sự kiện khi script bị vô hiệu hóa
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
