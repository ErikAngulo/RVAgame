using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigFormHandler : MonoBehaviour
{


    public GameObject myCanvasHolder;

    private Canvas _myCanvas;
    private string _test_game = "MainScene";

    // Start is called before the first frame update
    void Start()
    {
        _myCanvas = myCanvasHolder.gameObject.GetComponent<Canvas> ();

        //_button = GameObject.Find("buttonName").GetComponent<UnityEngine.UI.Button>();

        TMPro.TextMeshProUGUI text1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").Find("Dropdown1").GetComponent<Dropdown>();

        TMPro.TextMeshProUGUI text2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").GetComponent<TMPro.TextMeshProUGUI>();
        Dropdown dropdown2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").Find("Dropdown2").GetComponent<Dropdown>();

        if (StaticClass.SelectedGameScene.Equals(_test_game)){
            text1.text = "Time";
            List<string> list1 = new List<string> { "30", "60" };
            foreach (string option in list1)
            {
                dropdown1.options.Add(new Dropdown.OptionData(option));
            }
            dropdown1.value = 0;

            text2.text = "Ball speed";
            List<string> list2 = new List<string> { "10", "20", "30" };
            foreach (string option in list2)
            {
                dropdown2.options.Add(new Dropdown.OptionData(option));
            }
            dropdown2.value = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGameConfiguration(){
        Dropdown dropdown1 = _myCanvas.GetComponent<Transform>().Find("Input1Text").Find("Dropdown1").GetComponent<Dropdown>();
        Dropdown dropdown2 = _myCanvas.GetComponent<Transform>().Find("Input2Text").Find("Dropdown2").GetComponent<Dropdown>();

        StaticClass.Time = dropdown1.options[dropdown1.value].text;
        StaticClass.BallSpeed = dropdown2.options[dropdown2.value].text;

    }


}
