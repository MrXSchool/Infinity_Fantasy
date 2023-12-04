using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class saveSlotButton : MonoBehaviour
{
    public string nameSave;
    LoadingScript loadingScript;
    // Start is called before the first frame update
    void Start()
    {
        nameSave = this.gameObject.GetComponentInChildren<TMP_Text>().text;
        loadingScript = GameObject.Find("Loading").GetComponent<LoadingScript>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void click()
    {
        loadingScript.LoadLevel(getLevel(nameSave));
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
