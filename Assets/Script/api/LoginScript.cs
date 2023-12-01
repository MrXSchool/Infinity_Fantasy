using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using TMPro;





public class LoginScript : MonoBehaviour
{

    public TMP_InputField username;
    public TMP_InputField password;
    public bool clicked = false;
    public GameObject login;
    public GameObject register;
    public float x;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            chuyencanh();
        }

        x = login.transform.position.x;
        if (login.transform.position.x >= 20)
        {
            // login.SetActive(false);
            clicked = false;
            // register.SetActive(true);
        }
    }

    public void oke()
    {
        clicked = !clicked;
    }
    public void kiemtraDangNhap()
    {
        string user = username.text;
        string pass = password.text;

        LoginRequest loginRequest = new LoginRequest(user, pass);
        CheckLogin(loginRequest);
        StartCoroutine(CheckLogin(loginRequest));

    }

    IEnumerator CheckLogin(LoginRequest loginRequest)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(loginRequest);

        var request = new UnityWebRequest("http://localhost:6969/gameAPI/users/login", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStringRequest);
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
            // LoginRespone loginRespone = JsonConvert.DeserializeObject<LoginRespone>(jsonString);
            // Debug.Log(string.Format("LoginRespone:", loginRespone));
            Debug.Log(jsonString);


        }
        request.Dispose();


    }
    //ui
    public void chuyencanh()
    {

        transform.Translate(Vector3.right * 10 * Time.deltaTime);
        if (register.transform.position.x <= 0)
        {
            register.transform.Translate(Vector3.right * 10 * Time.deltaTime);
        }


    }
}
