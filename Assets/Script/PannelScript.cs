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
using System;
using System.IO;

public class PannelScript : MonoBehaviour
{
    public AudioClip clip;
    public GameObject Menu_inGame;
    public Canvas canvas;
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
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        // if (Menu_panel_after_login == null) { Menu_panel_after_login = GameObject.Find("Menu_panel_after_login"); }
        // Menu_panel_after_login.SetActive(currentScene.ToString() == "intro");
        if (allMusic == null) { allMusic = GameObject.Find("music"); }
        currentScene = SceneManager.GetActiveScene().name;
        // isLogin = controlAPI1.isLogin;
        if (Input.GetKeyDown(KeyCode.Escape) && currentScene != "intro")
        {
            Debug.Log("esc");
            isActived = !isActived;
            if (isActived)
            {
                Time.timeScale = 0;
                Menu_inGame.SetActive(true);
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
        player = GameObject.FindWithTag("Player");




        if (currentScene != "intro")
        {
            if (player.GetComponent<PlayerScript>().isDead)
            {
                StartCoroutine(DoFade2(Dead_panel.GetComponent<CanvasGroup>(), 0, 1));
            }
        }


        // List<TMP_InputField> inputFields = new List<TMP_InputField>();
        // inputFields.AddRange(GameObject.Find("Login").GetComponentsInChildren<TMP_InputField>());
        // inputFields.AddRange(GameObject.Find("Register").GetComponentsInChildren<TMP_InputField>());
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     foreach (TMP_InputField inputField in inputFields)
        //     {
        //         if (inputField.isFocused)
        //         {
        //             inputField.DeactivateInputField();
        //             inputField.transform.parent.GetComponentInChildren<TMP_InputField>().ActivateInputField();
        //         }
        //     }
        // }

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
        if (currentScene != "intro")
        {
            Menu_inGame.SetActive(false);
        }
        else
        {
            Menu_panel_after_login.SetActive(false);
        }
    }

    public void loadClick()
    {
        load.SetActive(true);
        if (currentScene != "intro")
        {
            Menu_inGame.SetActive(false);
        }
        else
        {
            Menu_panel_after_login.SetActive(false);
        }
    }

    public void saveClick()
    {
        save.SetActive(true);
        if (currentScene != "intro")
        {
            Menu_inGame.SetActive(false);
        }
        else
        {
            Menu_panel_after_login.SetActive(false);
        }
    }

    public void exitClick()
    {
        exit.SetActive(true);
        if (currentScene != "intro")
        {
            Menu_inGame.SetActive(false);
        }
        else
        {
            Menu_panel_after_login.SetActive(false);
        }
    }

    public void exitOke()
    {
        //chuyển về màn hình chính
        exit.SetActive(false);
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
                EnemyScript enemy = e.GetComponent<EnemyScript>();
                EnemyModel enemydata = new EnemyModel(enemy);
                enemydata.enemyName = enemydata.enemyName.Split('(')[0];
                enemies.Add(enemydata);
            }
            PlayerScript players = player.GetComponent<PlayerScript>();
            PlayerModel playerdata = new PlayerModel(players);
            MapModel mapdata = new MapModel(sceneName, playerdata, enemies);

            string filePath = Path.Combine(Application.dataPath, "Data", "user", $"{username}.json");

            // Đọc dữ liệu từ tệp cũ
            string json = System.IO.File.ReadAllText(filePath);
            User user = JsonConvert.DeserializeObject<User>(json);

            if (user.data == null)
            {
                Debug.Log("MapData: null");
                user.data = new List<MapModel> { mapdata };
            }
            else
            {
                bool isExist = false;
                foreach (MapModel map in user.data)
                {
                    if (map.sceneName == sceneName)
                    {
                        map.player = playerdata;
                        map.enermy = enemies;
                        isExist = true;
                        Debug.Log("Map đã được ghi đè");
                        break; // Thoát khỏi vòng lặp khi tìm thấy map giống
                    }
                }
                if (!isExist)
                {
                    user.data.Add(mapdata);
                    Debug.Log("Map đã được thêm mới");
                }
            }

            // Lưu dữ liệu vào tệp JSON
            string save = JsonConvert.SerializeObject(user);
            SaveData(username, save);
            txtDialog.text = "Lưu thành công!";
            dialogAnimation();
        }
        catch (Exception e)
        {
            Debug.LogError($"Có lỗi xảy ra khi save: {e.Message}");
            txtDialog.text = "Có lỗi xảy ra khi save";
            dialogAnimation();
        }

    }

    public void SyncClick()
    {
        string username = PlayerPrefs.GetString("username");
        SyncData(username);
        StartCoroutine(SyncData(username));
    }


    IEnumerator SyncData(string username)
    {

        var request = new UnityWebRequest("http://localhost:6969/gameAPI/users/" + username, "get");
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
                string jsonString1 = JsonConvert.SerializeObject(respone.user);
                SaveData(respone.user.username, jsonString1);
                txtDialog.text = "Đồng bộ dữ liệu thành công!";
            }
            else
            {

                txtDialog.text = respone.message;
            }
            dialogAnimation();




        }
        request.Dispose();


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
                    SaveData(username, save);
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

    IEnumerator DoFade1(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < 1f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter);
            yield return null;
            if (end == 0)
            {
                canvasGroup.interactable = (false);
                canvasGroup.gameObject.SetActive(false);

            }
            else
            {

                canvasGroup.gameObject.SetActive(true);
                canvasGroup.interactable = (true);
            }
        }
    }

    IEnumerator DoFade2(CanvasGroup canvasGroup, float start, float end)
    {
        if (!Dead_panel.activeSelf) { StartCoroutine(DoFade1(PlayAgian_panel.GetComponent<CanvasGroup>(), 0, 1)); }
        float counter = 0f;
        while (counter < 2f)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter);
            if (canvasGroup.alpha == 1)
            {
                StartCoroutine(DoFade2(canvasGroup, 1, 0));
            }

            yield return null;
            if (end == 0)
            {
                canvasGroup.interactable = (false);
                canvasGroup.gameObject.SetActive(false);

            }
            else
            {

                canvasGroup.gameObject.SetActive(true);
                canvasGroup.interactable = (true);
            }
        }
    }
    private void SaveData(string username, string saveData)
    {
        string directoryPath = Application.dataPath + "/Data/user/";

        // Kiểm tra nếu thư mục không tồn tại, tạo mới nó
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Tạo đường dẫn đầy đủ cho file
        string filePath = Path.Combine(directoryPath, username + ".json");

        // Ghi dữ liệu vào file
        File.WriteAllText(filePath, saveData);

        Debug.Log("Dữ liệu đã được lưu tại: " + filePath);
    }


    public void PlayAgian_panelBack()
    {
        PlayAgian_panel.SetActive(false);
        Menu_panel_after_login.SetActive(true);
        SceneManager.LoadScene("intro");
    }

    public void PlayAgian_panelOke()
    {
        PlayAgian_panel.SetActive(false);
        SceneManager.LoadScene(currentScene);
    }



}
