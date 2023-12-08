using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setingScript : MonoBehaviour
{
    public Slider musicVolume;
    public AudioSource musicBackground;
    public GameObject musicObject;
    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        musicObject = GameObject.Find("music");
        musicBackground = musicObject.GetComponentsInChildren<AudioSource>()[0];
        musicVolume.value = musicBackground.volume;
    }

    // Update is called once per frame
    void Update()
    {
        musicBackground.volume = musicVolume.value;

    }


}
