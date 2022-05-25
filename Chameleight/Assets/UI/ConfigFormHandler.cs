using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigFormHandler : MonoBehaviour
{


    public GameObject myCanvasHolder;

    private Canvas _myCanvas;
    private string _ball_game = "BallScene";
    private string _shooting_game = "ShootingScene";

    // Start is called before the first frame update
    void Start()
    {
        _myCanvas = myCanvasHolder.gameObject.GetComponent<Canvas> ();

        //_button = GameObject.Find("buttonName").GetComponent<UnityEngine.UI.Button>();

        TMPro.TextMeshProUGUI text1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").Find("Dropdown1").GetComponent<Dropdown>();

        TMPro.TextMeshProUGUI text2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").Find("Dropdown2").GetComponent<Dropdown>();

        TMPro.TextMeshProUGUI text3 = _myCanvas.GetComponent<Transform>().Find("Input3Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown3 = _myCanvas.GetComponent<Transform>().Find("Input3Text").Find("Dropdown3").GetComponent<Dropdown>();

        if (StaticClass.SelectedGameScene.Equals(_ball_game)){
            text1.text = "Game type (balls)";
            List<string> list1 = new List<string> { "QuickPlay (10)", "Normal (20)", "Marathon (30)" };
            foreach (string option in list1)
            {
                dropdown1.options.Add(new Dropdown.OptionData(option));
            }
            dropdown1.value = 0;

            text2.text = "Ball resistance";
            List<string> list2 = new List<string> { "Low", "Medium", "High" };
            foreach (string option in list2)
            {
                dropdown2.options.Add(new Dropdown.OptionData(option));
            }
            dropdown2.value = 0;

            text3.gameObject.SetActive(false);
            dropdown3.gameObject.SetActive(false);
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

            text3.text = "Select controller";
            List<string> list3 = new List<string> { "Left", "Right" };
            foreach (string option in list3)
            {
                dropdown3.options.Add(new Dropdown.OptionData(option));
            }
            dropdown3.value = 1;
        }

        dropdown1.RefreshShownValue();
        dropdown2.RefreshShownValue();
        dropdown3.RefreshShownValue();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGameConfiguration(){
        Dropdown dropdown1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").Find("Dropdown1").GetComponent<Dropdown>();
        Dropdown dropdown2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").Find("Dropdown2").GetComponent<Dropdown>();
        Dropdown dropdown3 = _myCanvas.GetComponent<Transform>().Find("Input3Text").Find("Dropdown3").GetComponent<Dropdown>();

        List<int> list1 = new List<int> { 10, 20, 30 };
        List<int> list2 = new List<int> { 60, 90, 120 };
        List<float> list3 = new List<float> { 3.5f, 2.5f, 1.5f };
        if (StaticClass.SelectedGameScene.Equals(_ball_game)){
            StaticClass.BallLimit = list1[dropdown1.value];
            StaticClass.BallFactor = list3[dropdown2.value];
        }
        else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
            StaticClass.Time = list2[dropdown2.value];
            if (dropdown1.value == 0){
                StaticClass.TargetMovement = true;
            }
            else if (dropdown1.value == 1){
                StaticClass.TargetMovement = false;
            }
            StaticClass.Controller = dropdown3.options[dropdown3.value].text;
        }

    }


}