using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI incorrectText;
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI outText;

    private int _correct = 0;
    private int _incorrect = 0;
    private int _missed = 0;
    private int _out = 0;

    private List<(int,string)> _resultList = new List<(int,string)>();

    public void Correct(int ball){
        _correct += 1;
        _resultList.Add((ball,"CORRECT"));
        correctText.text = "Correct: " + _correct;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void Incorrect(int ball){
        _incorrect += 1;
        _resultList.Add((ball,"INCORRECT"));
        incorrectText.text = "Incorrect: " + _incorrect;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void Missed(int ball){
        _missed += 1;
        _resultList.Add((ball,"MISSED"));
        missedText.text = "Missed: " + _missed;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void Out(int ball){
        _out += 1;
        _resultList.Add((ball,"OUT"));
        outText.text = "Out: " + _out;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public List<(int,string)> GetScores(){
        return _resultList;
    }
}
