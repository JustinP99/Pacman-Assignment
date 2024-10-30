using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioClip introMusic;
    public AudioClip normalStateMusic;
    private AudioSource audioSource;
    public float introDuration = 5f;//how many seconds

    // Start is called before the first frame update
    void Start()
    {
       //get audiosource component attatched to gameobject
       audioSource = GetComponent<AudioSource>();
        //play intro music
        playIntroMusic();
    }

    void playIntroMusic()
    {
        audioSource.clip = introMusic;
        audioSource.Play();

        // Schedule switching to the normal music when the intro finishes
        Invoke(nameof(playNormalMusic), introDuration);
    }

    void playNormalMusic()
    {
        audioSource.clip = normalStateMusic;
        audioSource.loop = true;  // Enable looping for the normal state music
        audioSource.Play();
    }

}
