using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public float time = 0.1f;
    public ScoreController scoreController;
    private bool _pushable = true;

    public void Pushed(GameObject go, GameObject ball, int ballNumber)
    {

        if(_pushable){
            if(ball != null){
                if(ball.GetComponent<Renderer>().material.color==go.GetComponent<Renderer>().material.color){
                    Debug.Log("Correct!");
                    scoreController.Correct(ballNumber,ball.GetComponent<Renderer>().material.name);
                }else{
                    Debug.Log("Incorrect!");
                    scoreController.Incorrect(ballNumber,ball.GetComponent<Renderer>().material.name);
                }
            }else{
                Debug.Log("Use the balls!");
            }
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
