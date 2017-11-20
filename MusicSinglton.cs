using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSinglton : MonoBehaviour
{
    //singleton

    public static MusicSinglton backgroundMusic = null;
    public static MusicSinglton Instance
    { get { return backgroundMusic; } }


    public AudioSource backgroundAudioSource;
    public AudioClip singletonClip;
    public AudioClip backgroundAudioClip;

    void Awake()
    {
        if (backgroundMusic != null && backgroundMusic != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            backgroundMusic = this;
            backgroundAudioSource = this.GetComponent<AudioSource>();
            backgroundAudioSource.Play();
        }
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void StopAudio()
    {
        backgroundAudioSource.Stop();
    }

    public void PlayAudio()
    {
        backgroundAudioSource = this.GetComponent<AudioSource>();
        backgroundAudioSource.Play();
    }
}
