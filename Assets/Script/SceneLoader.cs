using Newtonsoft.Json;
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
        if (isload)
        {
            string username = PlayerPrefs.GetString("username");
            string json = System.IO.File.ReadAllText(Application.dataPath + "/Data/user/" + username + ".json");
            User user = JsonConvert.DeserializeObject<User>(json);
            foreach (MapModel item in user.data)
            {
                if (item.sceneName == scene.name)
                {
                    loadGame.loadFromJson(item.sceneName);
                }
                else
                {
                    Debug.Log("mapsave không tồn tại");
                    SceneManager.LoadScene(loadGame.getLevel(scene.name));
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
