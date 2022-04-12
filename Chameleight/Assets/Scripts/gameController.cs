using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public AudioSource music;
    private List<GameObject> _lightBlue;
    private List<GameObject> _lightOrange;
    private string _currentLight;
    private int _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        _lightBlue = new List<GameObject>(GameObject.FindGameObjectsWithTag("BlueLight"));
        _lightOrange = new List<GameObject>(GameObject.FindGameObjectsWithTag("OrangeLight"));
        //music.Play();
    }

    public void TargetHit (GameObject go){
         Debug.Log("HOLA");
        if(_lightBlue.Contains(go)){
             _score += 10;
             _lightBlue.Remove(go);
             Debug.Log("Blue Light! Score:" + _score);
             scoreText.text = "Score: " + _score;
        }else if(_lightOrange.Contains(go)){
             _score += 20;
             _lightOrange.Remove(go);
             Debug.Log("Orange Light! Score:" + _score);
             scoreText.text = "Score: " + _score;
        }
    }
}