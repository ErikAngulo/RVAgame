using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SliderValueToText : MonoBehaviour {
public Slider sliderUI;
public string text = "";
private TextMeshProUGUI textSliderValue;

  //Initialize slider text.
  void Start (){
    textSliderValue = GetComponent<TextMeshProUGUI>();
    ShowSliderValue();
  }

  //Get slider value and update slider text.
  public void ShowSliderValue () {
    string sliderMessage = sliderUI.value.ToString() + " " + text;
    textSliderValue.text = sliderMessage;
  }
}