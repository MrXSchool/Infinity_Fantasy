using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

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
    public GameObject Confirm_panel;
    public GameObject Dead_panel;
    public GameObject PlayAgian_panel;
    public float x;
    public float y;
    public bool isLoginActive = true;
    public bool isLogin = false;

    public bool LoR = true;
    public TMP_InputField[] confirmOTP;
    public float timeWait = 60f;
    public GameObject sendAgain;
    public bool isSend = true;

    // Start is called before the first frame update
    void Start()
    {
        // register = GameObject.Find("Register_panel");
        // login = GameObject.Find("Login_panel");
        // dialog = GameObject.Find("dialog");
        // Menu_panel_after_login = GameObject.Find("Menu_panel_after_login");

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
        if (register == null) register = GameObject.Find("Register_panel");
        if (login == null) login = GameObject.Find("Login_panel");
        if (dialog == null) dialog = GameObject.Find("dialog");
        if (Menu_panel_after_login == null) Menu_panel_after_login = GameObject.Find("Menu_panel_after_login");
        if (confirmOTP == null) confirmOTP = Confirm_panel.GetComponentsInChildren<TMP_InputField>();
        isLoginActive = login.activeSelf;




        if (!LoR)
        {
            //fade
        }

        if (Confirm_panel.activeSelf)
        {
            confirmOTP[0].onValueChanged.AddListener(OnFirstInputValueChanged);
            sendAgain = GameObject.Find("sendAgain");
            sendAgain.GetComponentInChildren<TMP_Text>().text = timeWait.ToString();


            if (timeWait <= 0)
            {

                sendAgain.GetComponent<Button>().interactable = true;
                sendAgain.GetComponentInChildren<TMP_Text>().text = "Send Again";
                timeWait = 60f;
                isSend = false;
            }
            if (!sendAgain.GetComponent<Button>().IsInteractable() && isSend)
            {
                timeWait -= Time.deltaTime;
            }
        }

        Menu_panel_after_login.SetActive(isLogin);

        if (Menu_panel_after_login.GetComponent<CanvasGroup>().alpha == 1)
        {
            Menu_panel_after_login.GetComponent<RectTransform>().position = new Vector3(0, 0, 0);
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
                txtDialog.text = "Wellcome " + respone.user.username + " !";
                PlayerPrefs.SetString("_id", respone.user._id);
                PlayerPrefs.SetString("email", respone.user.email);
                CanvasGroup cgR = register.GetComponent<CanvasGroup>();
                CanvasGroup cgCF = Confirm_panel.GetComponent<CanvasGroup>();
                StartCoroutine(DoFade1(cgR, cgR.alpha, cgR.alpha - cgR.alpha));
                StartCoroutine(DoFade1(cgCF, cgCF.alpha, 1));

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
                string jsonString1 = JsonConvert.SerializeObject(respone.user);
                System.IO.File.WriteAllText(Application.dataPath + "/Data/user/" + respone.user.username + ".json", jsonString1);
                txtDialog.text = "Long time no see " + respone.user.username + " !";
                PlayerPrefs.SetString("_id", respone.user._id);
                PlayerPrefs.SetString("username", respone.user.username);
                login.SetActive(false);
                isLogin = true;
                GameObject.Find("Button_register").SetActive(false);
                if (isLogin)
                {
                    StartCoroutine(DoFade1(Menu_panel_after_login.GetComponent<CanvasGroup>(), 0, 1));
                }
            }
            else
            {

                txtDialog.text = respone.message;
            }
            dialogAnimation();




        }
        request.Dispose();


    }

    public void verifyEmail()
    {
        try
        {
            string email = PlayerPrefs.GetString("email");
            string otp = "";
            foreach (TMP_InputField item in confirmOTP)
            {
                otp += item.text;

            }
            Debug.Log(otp);
            Verify(email, otp);
            StartCoroutine(Verify(email, otp));
        }
        catch (System.Exception)
        {

            throw;
        }
    }


    IEnumerator Verify(string email, string otp)
    {

        var request = new UnityWebRequest("http://localhost:6969/gameAPI/verify/" + otp + "?email=" + email, "GET");
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
                txtDialog.text = respone.message;
                CanvasGroup cgCF = Confirm_panel.GetComponent<CanvasGroup>();
                StartCoroutine(DoFade1(cgCF, cgCF.alpha, cgCF.alpha - cgCF.alpha));
                CanvasGroup cgL = login.GetComponent<CanvasGroup>();
                StartCoroutine(DoFade1(cgL, cgL.alpha, 1));
            }
            else
            {

                txtDialog.text = respone.message;
            }
            dialogAnimation();




        }
        request.Dispose();
    }

    public void backOTP()
    {
        Confirm_panel.SetActive(false);
        register.SetActive(true);
    }

    public void sendOTPAgain()
    {
        string email = PlayerPrefs.GetString("email");
        reSendOTP(email);
        StartCoroutine(reSendOTP(email));
        sendAgain.GetComponent<Button>().interactable = false;

    }
    IEnumerator reSendOTP(string email)
    {

        var request = new UnityWebRequest("http://localhost:6969/gameAPI/reSendOTP/" + email, "GET");
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
                txtDialog.text = respone.message;
            }
            else
            {

                txtDialog.text = respone.message;
            }
            dialogAnimation();
        }
        request.Dispose();
    }

    //logic otp field
    void OnFirstInputValueChanged(string newValue)
    {
        // Kiểm tra nếu có thay đổi trong ô đầu tiên
        if (newValue.Length > 0)
        {
            // Lặp qua các ô còn lại và điền số vào chúng
            for (int i = 0; i < confirmOTP.Length; i++)
            {
                // Kiểm tra không vượt quá độ dài của chuỗi mới
                if (i < newValue.Length)
                {
                    confirmOTP[i].text = newValue[i].ToString();
                }
                else
                {
                    confirmOTP[i].text = string.Empty;
                }
            }
        }
    }

    //ui
    public void chuyencanh(float speed)
    {

        login.transform.Translate(Vector3.right * speed * Time.deltaTime);
        register.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void swithLoR()
    {
        LoR = !LoR;
        CanvasGroup cgL = login.GetComponent<CanvasGroup>();
        CanvasGroup cgR = register.GetComponent<CanvasGroup>();
        if (LoR)
        {
            GameObject.Find("Button_register").GetComponentInChildren<TMP_Text>().text = "No account? Register now";
            if (cgR.alpha != 0)
            {
                StartCoroutine(DoFade1(cgR, cgR.alpha, 0));
            }
            StartCoroutine(DoFade1(cgL, cgL.alpha, 1));

        }
        else
        {
            if (cgL.alpha != 0)
            {
                StartCoroutine(DoFade1(cgL, cgL.alpha, 0));
            }
            StartCoroutine(DoFade1(cgR, cgR.alpha, 1));
            GameObject.Find("Button_register").GetComponentInChildren<TMP_Text>().text = "Already account? Login now";
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




}