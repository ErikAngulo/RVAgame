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

    private List<(int,string)> _colorList = new List<(int,string)>();

    public void Correct(int ball, string material){
        _correct += 1;
        _resultList.Add((ball,"CORRECT"));
        _colorList.Add((ball,material));
        correctText.text = "Correct: " + _correct;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void Incorrect(int ball, string material){
        _incorrect += 1;
        _resultList.Add((ball,"INCORRECT"));
        _colorList.Add((ball,material));
        incorrectText.text = "Incorrect: " + _incorrect;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void Missed(int ball, string material){
        _missed += 1;
        _resultList.Add((ball,"MISSED"));
        _colorList.Add((ball,material));
        missedText.text = "Missed: " + _missed;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public void Out(int ball, string material){
        _out += 1;
        _resultList.Add((ball,"OUT"));
        _colorList.Add((ball,material));
        outText.text = "Out: " + _out;
        foreach (var item in _resultList)
        {
            Debug.Log(item.ToString());
        }
    }

    public List<(int,string)> GetScores(){
        return _resultList;
    }

    public List<(int,string)> GetColors(){
        return _colorList;
    }
}
