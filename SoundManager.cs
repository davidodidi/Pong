using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // Holds the single instance of the SoundManager that 
    // you can access from any script
    public static SoundManager Instance = null; //This is a singleton so that I have only one Instance

    // All sound effects in the game
    // All are public so you can set them in the Inspector
    public AudioClip goalBloop;
    public AudioClip lossBuzz;
    public AudioClip hitPaddleBloop; //different sounds
    public AudioClip winSound;
    public AudioClip wallBloop;

    // Refers to the audio source added to the SoundManager
    // to play sound effects
    private AudioSource soundEffectAudio; //the source where audio comes form
    

    // Use this for initialization
    void Start () {

        // This is a singleton that makes sure you only
        // ever have one Sound Manager
        // If there is any other Sound Manager created destroy it
        if (Instance == null)
        {
            Instance = this;
        } else if (Instance != this) //if instance isnt soundManger
        {
            Destroy(gameObject); //if it isn't this specific sound manager then destroy
        }

        AudioSource[] sources = GetComponents<AudioSource>(); //This stores all the different sound effects

        foreach(AudioSource source in sources)
        {
            if(source.clip == null)
            {
                soundEffectAudio = source; 
            }
        }
	}
	
	public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }


}
