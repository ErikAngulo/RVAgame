using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class SliderValueToText : MonoBehaviour {
public Slider sliderUI;
public string text = "";
private TextMeshProUGUI textSliderValue;

  void Start (){
    textSliderValue = GetComponent<TextMeshProUGUI>();
    ShowSliderValue();
  }

  public void ShowSliderValue () {
    string sliderMessage = sliderUI.value.ToString() + " " + text;
    Debug.Log(sliderMessage);
    textSliderValue.text = sliderMessage;
  }
}