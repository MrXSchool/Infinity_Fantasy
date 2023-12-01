using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PannelScript : MonoBehaviour
{
    public AudioClip clip;
    //volume
    [Range(0, 1)]
    public float volume;

    private void Awake()
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
