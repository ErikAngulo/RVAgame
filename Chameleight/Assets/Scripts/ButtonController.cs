using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    public float time = 0.1f;
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI incorrectText;
    public TextMeshProUGUI timeText;
    private bool _pushable = true;
    private int _correct = 0;
    private int _incorrect = 0;
    private float _time = 0.0f;

    public void Start(){
        _time = Time.time;
    }

    public void Update(){
        float act_time = Time.time-_time;
        timeText.text = "Time: " + act_time.ToString("0.0") + "s";
    }

    public void Pushed(GameObject go, GameObject ball)
    {
        if(ball != null){
            if(ball.GetComponent<Renderer>().material.color==go.GetComponent<Renderer>().material.color){
                Debug.Log("Correct!");
                _correct += 1;
                correctText.text = "Correct: " + _correct;
                _time = Time.time;
                timeText.text = "Time: 0.0s";
            }else{
                Debug.Log("Incorrect!");
                _incorrect += 1;
                incorrectText.text = "Incorrect: " + _incorrect;
            }
        }else{
            Debug.Log("Use the balls!");
        }
        if(_pushable){
            StartCoroutine(Push(time,go));
        }
    }

    IEnumerator Push(float time, GameObject go)
    {
        _pushable = false;
        Vector3 startingPos  = go.transform.position;
        Vector3 finalPos = go.transform.position + (transform.forward * 0.05f);
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            go.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < time)
        {
            go.transform.position = Vector3.Lerp(finalPos, startingPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        go.transform.position = startingPos;
        _pushable = true;
    }
}
