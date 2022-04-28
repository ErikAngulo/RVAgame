using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigFormHandler : MonoBehaviour
{


    public GameObject myCanvasHolder;

    private Canvas _myCanvas;
    private string _ball_game = "MainScene";
    private string _shooting_game = "Chameleight_Scenary";

    // Start is called before the first frame update
    void Start()
    {
        _myCanvas = myCanvasHolder.gameObject.GetComponent<Canvas> ();

        //_button = GameObject.Find("buttonName").GetComponent<UnityEngine.UI.Button>();

        TMPro.TextMeshProUGUI text1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").Find("Dropdown1").GetComponent<Dropdown>();

        TMPro.TextMeshProUGUI text2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").Find("Dropdown2").GetComponent<Dropdown>();

        if (StaticClass.SelectedGameScene.Equals(_ball_game)){
            text1.text = "Ball limit";
            List<string> list1 = new List<string> { "5", "10", "15" };
            foreach (string option in list1)
            {
                dropdown1.options.Add(new Dropdown.OptionData(option));
            }
            dropdown1.value = 0;

            text2.text = "Ball speed";
            List<string> list2 = new List<string> { "8", "14", "20" };
            foreach (string option in list2)
            {
                dropdown2.options.Add(new Dropdown.OptionData(option));
            }
            dropdown2.value = 0;
        }
        else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
            text1.text = "Movement";
            List<string> list1 = new List<string> { "Yes", "No" };
            foreach (string option in list1)
            {
                dropdown1.options.Add(new Dropdown.OptionData(option));
            }
            dropdown1.value = 0;

            text2.text = "Time limit";
            List<string> list2 = new List<string> { "60", "90", "120" };
            foreach (string option in list2)
            {
                dropdown2.options.Add(new Dropdown.OptionData(option));
            }
            dropdown2.value = 0;
        }

        dropdown1.RefreshShownValue();
        dropdown2.RefreshShownValue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGameConfiguration(){
        Dropdown dropdown1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").Find("Dropdown1").GetComponent<Dropdown>();
        Dropdown dropdown2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").Find("Dropdown2").GetComponent<Dropdown>();

        if (StaticClass.SelectedGameScene.Equals(_ball_game)){
            StaticClass.BallLimit = int.Parse(dropdown1.options[dropdown1.value].text);
            StaticClass.BallSpeed = float.Parse(dropdown2.options[dropdown2.value].text);
        }
        else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
            StaticClass.Time = float.Parse(dropdown2.options[dropdown2.value].text);;
            if (dropdown1.value == 0){
                StaticClass.TargetMovement = true;
            }
            else if (dropdown1.value == 1){
                StaticClass.TargetMovement = false;
            }
        }

    }


}
