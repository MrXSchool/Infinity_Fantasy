using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;

public class controlAPI : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;

    public TMP_InputField email, username1, password1, confirmPassword;
    public bool clicked = false;
    public GameObject login;
    public GameObject register;
    public float x;
    // Start is called before the first frame update
    void Start()
    {
        register = GameObject.Find("Register_panel");
        login = GameObject.Find("Login_panel");

        email = register.GetComponentsInChildren<TMP_InputField>()[0];
        username1 = register.GetComponentsInChildren<TMP_InputField>()[1];
        password1 = register.GetComponentsInChildren<TMP_InputField>()[2];
        confirmPassword = register.GetComponentsInChildren<TMP_InputField>()[3];

        username = login.GetComponentsInChildren<TMP_InputField>()[0];
        password = login.GetComponentsInChildren<TMP_InputField>()[1];

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

    public void kiemtraDangKy()
    {
        string user = username1.text;
        string pass = password1.text;
        string email1 = email.text;
        string confirm = confirmPassword.text;

        RegisterRequest registerRequest = new RegisterRequest(email1, pass, user, confirm);
        CheckRegister(registerRequest);
        StartCoroutine(CheckRegister(registerRequest));

    }

    IEnumerator CheckRegister(RegisterRequest registerRequest)
    {
        string jsonStringRequest = JsonConvert.SerializeObject(registerRequest);

        var request = new UnityWebRequest("http://localhost:6969/gameAPI/users/register", "POST");
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
            error error1 = JsonConvert.DeserializeObject<error>(jsonString);
            Debug.Log(error1.message);
        }
        request.Dispose();


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

        login.transform.Translate(Vector3.right * 10 * Time.deltaTime);
        if (register.transform.position.x <= 0)
        {
            register.transform.Translate(Vector3.right * 10 * Time.deltaTime);
        }


    }
}