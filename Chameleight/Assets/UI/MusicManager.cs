using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager: MonoBehaviour
{
    // Singleton class to load a single instance of MusicManager class object
    // The instance is static to access at any point of game

    // This is used to control the background music across menu scenes
    // This way the song does not restart while navigating menu scenes

    private static MusicManager _instance;
 
    //This way the instance is obtained
    public static MusicManager instance
    {
        get
        {
            return _instance;
        }
    }
 
    // This method is executed as soon as the attached GameObject loads
    // The game object is located at startup screen so the instance is loaded at game launch
    void Awake() 
    {
        // When first screen loads create the instance
        if(_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);

            _instance = GameObject.FindObjectOfType<MusicManager>();

            //Tell unity not to destroy this object when loading a new scene!
            DontDestroyOnLoad(_instance.gameObject);

            // Add Audio Source
            AudioSource audioSource = _instance.gameObject.AddComponent<AudioSource>();
            audioSource.clip = (AudioClip)Resources.Load("10. Boundless Slumber");
            audioSource.loop = true;
            _instance.play();
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if(this != _instance)
                Destroy(this.gameObject);
        }
    }

    // Play the menu background music if it is not already playing
    public void play(){
        if (_instance.gameObject.GetComponent<AudioSource>().isPlaying) return;
         _instance.gameObject.GetComponent<AudioSource>().Play();
    }

    // Stop the menu background music
    public void stop(){
        _instance.gameObject.GetComponent<AudioSource>().Stop();
    }
}
