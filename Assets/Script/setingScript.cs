using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setingScript : MonoBehaviour
{
    public Slider musicVolume;
    public AudioSource music;
    public AudioClip testsound;
    public bool isActived = false;

    // Start is called before the first frame update
    void Start()
    {
        musicVolume.value = music.volume;
    }

    // Update is called once per frame
    void Update()
    {
        music.volume = musicVolume.value;


    }


}
