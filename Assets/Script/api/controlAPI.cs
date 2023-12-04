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
    public TMP_Text txtDialog;

    public TMP_InputField email, username1, password1, confirmPassword;
    public bool clicked = false;
    public GameObject login;
    public GameObject register;
    public GameObject dialog;
    public GameObject Menu_panel_after_login;
    public float x;
    public float y;
    public bool isLoginActive = true;
    public bool isLogin = false;

    // Start is called before the first frame update
    void Start()
    {
        register = GameObject.Find("Register_panel");
        login = GameObject.Find("Login_panel");
        dialog = GameObject.Find("dialog");
        Menu_panel_after_login = GameObject.Find("Menu_panel_after_login");

        email = register.GetComponentsInChildren<TMP_InputField>()[0];
        username1 = register.GetComponentsInChildren<TMP_InputField>()[1];
        password1 = register.GetComponentsInChildren<TMP_InputField>()[2];
        confirmPassword = register.GetComponentsInChildren<TMP_InputField>()[3];

        username = login.GetComponentsInChildren<TMP_InputField>()[0];
        password = login.GetComponentsInChildren<TMP_InputField>()[1];
        txtDialog = dialog.GetComponentsInChildren<TMP_Text>()[0];

    }

    // Update is called once per frame
    void Update()
    {
        isLoginActive = login.activeSelf;
        if (!login.activeSelf && Menu_panel_after_login.transform.position.y <= 0)
        {
            showmenu();
        }
        if (clicked)
        {
            chuyencanh();
        }

        x = login.transform.position.x;
        y = Menu_panel_after_login.transform.position.y;
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

            Respone respone = JsonConvert.DeserializeObject<Respone>(jsonString);
            Debug.Log(string.Format("Respone status: {0}, message: {1}", respone.status, respone.message));
            if (respone.status)
            {
                txtDialog.text = "Wellcome " + respone.data.username + " !";
            }
            else
            {

                txtDialog.text = respone.message;
            }
            dialogAnimation();



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
            Respone respone = JsonConvert.DeserializeObject<Respone>(jsonString);

            if (respone.status)
            {
                string jsonString1 = JsonConvert.SerializeObject(respone.data);
                System.IO.File.WriteAllText(Application.dataPath + "/Data/user/" + respone.data.username + ".json", jsonString1);
                txtDialog.text = "Long time no see " + respone.data.username + " !";
                login.SetActive(false);
                isLogin = true;


            }
            else
            {

                txtDialog.text = respone.message;
            }
            dialogAnimation();




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


    public void dialogAnimation()
    {
        CanvasGroup canvasGroup = dialog.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, 1));


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

    public void showmenu()
    {
        Menu_panel_after_login.transform.Translate(Vector3.up * 10 * Time.deltaTime);

    }

}