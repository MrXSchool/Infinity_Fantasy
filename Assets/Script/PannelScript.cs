using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //
using TMPro;
using System.Threading;
using UnityEditor.PackageManager;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;
using Unity.VisualScripting;

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
    public GameObject Menu_panel_after_login;
    public string currentScene;
    LoadingScript loadingScript;
    public bool isLogin = false;
    controlAPI controlAPI1;
    public GameObject player;
    public GameObject[] enemy;
    public GameObject dialog;
    public TMP_Text txtDialog;

    private static PannelScript instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // Đảm bảo rằng GameController không bị hủy khi chuyển Scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Menu_panel_after_login = GameObject.Find("Menu_panel_after_login");
        txtDialog = dialog.GetComponentsInChildren<TMP_Text>()[0];
    }

    // Update is called once per frame
    void Update()
    {
        Menu_panel_after_login.SetActive(currentScene.ToString() == "intro");
        allMusic = GameObject.Find("music");
        currentScene = SceneManager.GetActiveScene().name;
        // isLogin = controlAPI1.isLogin;
        if (Input.GetKeyDown(KeyCode.Escape) && currentScene != "intro")
        {
            Debug.Log("esc");
            isActived = !isActived;
            if (!isActived)
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
        Menu_panel_after_login.SetActive(false);
    }

    public void loadClick()
    {
        load.SetActive(true);
        menu.SetActive(false);
        Menu_panel_after_login.SetActive(false);
    }

    public void saveClick()
    {
        save.SetActive(true);
        menu.SetActive(false);
        Menu_panel_after_login.SetActive(false);
    }

    public void exitClick()
    {
        exit.SetActive(true);
        menu.SetActive(false);
        Menu_panel_after_login.SetActive(false);
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
            if (currentScene == "intro")
            {
                Menu_panel_after_login.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
            }

        }
        else if (load.activeSelf)
        {
            load.SetActive(false);
            if (currentScene == "intro")
            {
                Menu_panel_after_login.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
            }
        }
        else if (save.activeSelf)
        {
            save.SetActive(false);
            if (currentScene == "intro")
            {
                Menu_panel_after_login.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
            }
        }
        else if (exit.activeSelf)
        {
            exit.SetActive(false);
            if (currentScene == "intro")
            {
                Menu_panel_after_login.SetActive(true);
            }
            else
            {
                menu.SetActive(true);
            }
        }
    }

    public void saveToJson()
    {
        try
        {
            Time.timeScale = 0;
            string username = PlayerPrefs.GetString("username");
            string sceneName = SceneManager.GetActiveScene().name;
            player = GameObject.FindGameObjectWithTag("Player");
            enemy = GameObject.FindGameObjectsWithTag("enemy");
            List<EnemyModel> enemies = new List<EnemyModel>();
            foreach (GameObject e in enemy)
            {
                Enemy enemy = e.GetComponent<Enemy>();

                EnemyModel enemydata = new EnemyModel(enemy);
                enemydata.enemyName = enemydata.enemyName.Split('(')[0];
                enemies.Add(enemydata);
            }
            PlayerScript players = this.player.GetComponent<PlayerScript>();
            PlayerModel playerdata = new PlayerModel(players);
            MapModel mapdata = new MapModel(sceneName, playerdata, enemies);
            //load dữ liệu từ file savc cũ và xem đã có trong đó hay chưa
            string json = System.IO.File.ReadAllText(Application.dataPath + "/Data/user/" + username + ".json");
            User user = JsonConvert.DeserializeObject<User>(json);
            List<MapModel> mapModels = user.data;
            bool isExist = false;
            foreach (MapModel map in mapModels)
            {
                if (map.sceneName == sceneName)
                {
                    map.player = playerdata;
                    map.enermy = enemies;
                    isExist = true;
                    Debug.Log("Map đã được ghi đè");
                }
            }
            if (!isExist)
            {
                mapModels.Add(mapdata);
                Debug.Log("Map đã được thêm mới");
            }

        }
        catch
        {
            Debug.Log("lỗi save file");
        }




    }

    public void dialogAnimation()
    {
        CanvasGroup canvasGroup = dialog.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1));


    }

    public void uploadClick()
    {
        uploadFile();
        StartCoroutine(uploadFile());
    }

    IEnumerator uploadFile()
    {
        string username = PlayerPrefs.GetString("username");
        string data = System.IO.File.ReadAllText(Application.dataPath + "/Data/user/" + username + ".json");
        User user = JsonConvert.DeserializeObject<User>(data);
        string body = JsonConvert.SerializeObject(user.data);
        var request = new UnityWebRequest("http://localhost:6969/gameAPI/users/saveGame/" + username, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {

            var jsonString = request.downloadHandler.text.ToString();
            Respone respone = JsonConvert.DeserializeObject<Respone>(jsonString);

            if (respone.status)
            {
                if (respone.user != user)
                {
                    string save = JsonConvert.SerializeObject(respone.user);
                    System.IO.File.WriteAllText(Application.dataPath + "/Data/user/" + username + ".json", save);
                    txtDialog.SetText("Upload file thành công");
                }
            }
            else
            {
                txtDialog.SetText(respone.message);
            }
            dialogAnimation();

        }

    }

    IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < 3f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter);
            if (canvasGroup.alpha == 1)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 0));
            }
            yield return null;

        }
    }




}
