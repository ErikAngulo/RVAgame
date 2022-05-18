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
            text1.text = "Game type (balls)";
            List<string> list1 = new List<string> { "QuickPlay (10)", "Normal (20)", "Marathon (30)" };
            foreach (string option in list1)
            {
                dropdown1.options.Add(new Dropdown.OptionData(option));
            }
            dropdown1.value = 0;

            text2.gameObject.SetActive(false);
            dropdown2.gameObject.SetActive(false);
        }
        else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
            text1.text = "Target movement";
            List<string> list1 = new List<string> { "Yes", "No" };
            foreach (string option in list1)
            {
                dropdown1.options.Add(new Dropdown.OptionData(option));
            }
            dropdown1.value = 0;

            text2.text = "Game type (time)";
            List<string> list2 = new List<string> { "QuickPlay (60)", "Normal (90)", "Marathon (120)" };
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

        List<int> list1 = new List<int> { 10, 20, 30 };
        List<int> list2 = new List<int> { 60, 90, 120 };
        if (StaticClass.SelectedGameScene.Equals(_ball_game)){
            StaticClass.BallLimit = list1[dropdown1.value];
        }
        else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
            StaticClass.Time = list2[dropdown2.value];
            if (dropdown1.value == 0){
                StaticClass.TargetMovement = true;
            }
            else if (dropdown1.value == 1){
                StaticClass.TargetMovement = false;
            }
        }

    }


}
