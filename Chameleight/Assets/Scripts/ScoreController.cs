using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI correctText;
    public TextMeshProUGUI incorrectText;
    public TextMeshProUGUI missedText;
    public TextMeshProUGUI outText;
    public List<AudioSource> booAudio = new List<AudioSource>();
    public List<AudioSource> clapAudio = new List<AudioSource>();

    private int _correct = 0;
    private int _incorrect = 0;
    private int _missed = 0;
    private int _out = 0;

    private List<(int,string)> _resultList = new List<(int,string)>();

    private List<(int,string)> _colorList = new List<(int,string)>();

    //Correct throw.
    public void Correct(int ball, string material){
        //Random correct sound.
        int s = Random.Range(0, clapAudio.Count);
        clapAudio.ElementAt(s).Play();
        _correct += 1;
        //Add result.
        _resultList.Add((ball,"CORRECT"));
        _colorList.Add((ball,material));
        correctText.text = "Correct: " + _correct;
    }

    //Incorrect throw.
    public void Incorrect(int ball, string material){
        //Random incorrect sound.
        int s = Random.Range(0, booAudio.Count);
        booAudio.ElementAt(s).Play();
        _incorrect += 1;
        //Add result.
        _resultList.Add((ball,"INCORRECT"));
        _colorList.Add((ball,material));
        incorrectText.text = "Incorrect: " + _incorrect;
    }

    //Missed throw.
    public void Missed(int ball, string material){
        //Random incorrect sound.
        int s = Random.Range(0, booAudio.Count);
        booAudio.ElementAt(s).Play();
        _missed += 1;
        //Add result.
        _resultList.Add((ball,"MISSED"));
        _colorList.Add((ball,material));
        missedText.text = "Missed: " + _missed;
    }

    //Out throw.
    public void Out(int ball, string material){
        //Random incorrect sound.
        int s = Random.Range(0, booAudio.Count);
        booAudio.ElementAt(s).Play();
        _out += 1;
        //Add result.
        _resultList.Add((ball,"OUT"));
        _colorList.Add((ball,material));
        outText.text = "Out: " + _out;
    }

    //Get scores.
    public List<(int,string)> GetScores(){
        return _resultList;
    }

    //Get ball colors.
    public List<(int,string)> GetColors(){
        return _colorList;
    }
}
