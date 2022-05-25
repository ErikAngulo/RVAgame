using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager: MonoBehaviour
{
    private static MusicManager _instance;
 
    public static MusicManager instance
    {
        get
        {
            return _instance;
        }
    }
 
    void Awake() 
    {
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

    public void play(){
        if (_instance.gameObject.GetComponent<AudioSource>().isPlaying) return;
         _instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public void stop(){
        _instance.gameObject.GetComponent<AudioSource>().Stop();
    }
}
