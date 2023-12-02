using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlSystemScript : MonoBehaviour
{
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneName == "intro")
        {
            checkLogin();
        }

    }

    public void checkLogin()
    {
        //check login
        //if login active menu
        //else active login
    }

    public void Observer()
    {
        //lấy thông tin người chơi
        //lấy thông tin vật phẩm
        //lấy thông tin nhiệm vụ
        //lấy thông tin kỹ năng
        //lấy thông tin map
        //lấy thông tin vị trí
        //lấy thông tin trạng thái
        //lấy thông tin thời gian
        //lấy thông tin quái
        //lấy thông tin npc
    }
}
