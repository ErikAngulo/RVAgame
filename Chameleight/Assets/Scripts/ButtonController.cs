using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public float time = 0.1f;
    private bool _pushable = true;
    public void Pushed(GameObject go)
    {
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
