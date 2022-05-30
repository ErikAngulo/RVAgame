using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public float time = 0.1f;
    public ScoreController scoreController;
    private bool _pushable = true;

    //Push the button using a ball.
    public void Pushed(GameObject go, GameObject ball, int ballNumber)
    {
        //Check if the button is pushable.
        if(_pushable){
            //Check if the object pushing the button is a ball.
            if(ball != null){
                //If the object pushing the button is a ball, check if the their colors match.
                if(ball.GetComponent<Renderer>().material.color==go.GetComponent<Renderer>().material.color){
                    //Colors match, correct.
                    scoreController.Correct(ballNumber,ball.GetComponent<Renderer>().material.name);
                }else{
                    //Colors don't match, incorrect.
                    scoreController.Incorrect(ballNumber,ball.GetComponent<Renderer>().material.name);
                }
            }
            //Button pushing animation.
            StartCoroutine(Push(time,go));
        }
    }

    //Button pushing animation.
    IEnumerator Push(float time, GameObject go)
    {
        _pushable = false;
        Vector3 startingPos  = go.transform.position;
        Vector3 finalPos = go.transform.position + (transform.forward * 0.05f);
        float elapsedTime = 0;

        //The button moves backwards.
        while (elapsedTime < time)
        {
            go.transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        //The button returns to its original location.
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
