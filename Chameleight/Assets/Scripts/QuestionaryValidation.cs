using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.Globalization;

public static class PlayerStats
{
    public static string player_name { get; set; }
    public static string email { get; set; }
    public static DateTime birthday { get; set; }
    public static string gender { get; set; }
    public static string laterality { get; set; }
    public static string sport { get; set; }
    public static string level { get; set; }
    public static int competing_years { get; set; }
    public static int height { get; set; }
    public static int weight { get; set; }
}

public class QuestionaryValidation : MonoBehaviour
{
    public TMP_InputField player_name;
    public TMP_InputField email;
    public TMP_InputField day;
    public TMP_InputField year;
    public TMP_InputField competing_years;
    public TMP_Dropdown month;
    public TMP_Dropdown sport;
    public ToggleGroup gender;
    public ToggleGroup laterality;
    public ToggleGroup level;
    public Slider height;
    public Slider weight;
    public Text warning;
    public ButtonHandler buttonHandler;
    // Start is called before the first frame update

    private bool IsValidEmailAddress(string s)
    {
	    var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
	    return regex.IsMatch(s);
    }
    public void Continue(){
        if(player_name.text.Length == 0 || email.text.Length == 0 || day.text.Length == 0 || year.text.Length == 0){
            warning.text = "Please, fill the required fields.";
            return;
        }
        if(!IsValidEmailAddress(email.text)){
            warning.text = "Please, enter a valid email address.";
            return;
        }
        string y = year.text;
        int current = int.Parse(DateTime.Now.ToString("yyyy"));
        if(int.Parse(y)<current-100 || int.Parse(y)>=current){
            warning.text = "Please, enter a valid birthday between the years " + (current-100).ToString() + " and " + (current-1).ToString() + ".";
            return;
        }
        while(y.Length<4){
            y = y.Insert(0, "0");
        }
        string m = (month.value+1).ToString();
        while(m.Length<2){
            m = m.Insert(0, "0");
        }
        string d = day.text;
        while(d.Length<2){
            d = d.Insert(0, "0");
        }
        string date = y+"/"+m+"/"+d;
        CultureInfo enUS = new CultureInfo("en-US");
        DateTime dateVal;
        if(!DateTime.TryParseExact(date,"yyyy/MM/dd", enUS, DateTimeStyles.None, out dateVal)){
            warning.text = "Please, enter a valid birthday.";
            return;
        }
        warning.text = "";

        PlayerStats.player_name = player_name.text;
        PlayerStats.email = email.text;
        PlayerStats.birthday = dateVal;
        IEnumerator<Toggle> toggles = gender.ActiveToggles().GetEnumerator();
        toggles.MoveNext();
        PlayerStats.gender = toggles.Current.GetComponentInChildren<Text>().text;
        toggles = laterality.ActiveToggles().GetEnumerator();
        toggles.MoveNext();
        PlayerStats.laterality = toggles.Current.GetComponentInChildren<Text>().text;
        buttonHandler.ChangeScene("Initial Questions 2");
    }

    public void Register(){

        if(competing_years.text.Length == 0){
            warning.text = "Please, fill the required fields.";
            return;
        }

        if((int) height.value == 0){
            warning.text = "Height value can't be 0.";
            return;
        }

        if((int) weight.value == 0){
            warning.text = "Weight value can't be 0.";
            return;
        }

        DateTime today = DateTime.Today;

        int age = today.Year - PlayerStats.birthday.Year;

        if (PlayerStats.birthday > today.AddYears(-age)) age--;

        if(int.Parse(competing_years.text)>age){
            warning.text = "Number of competing years must be less than or equal to your age.";
            return;
        }

        warning.text = "";

        PlayerStats.sport = sport.options[sport.value].text;
        IEnumerator<Toggle> toggles = level.ActiveToggles().GetEnumerator();
        toggles.MoveNext();
        PlayerStats.level = toggles.Current.GetComponentInChildren<Text>().text;
        PlayerStats.competing_years = int.Parse(competing_years.text);
        PlayerStats.height = (int) height.value;
        PlayerStats.weight = (int) weight.value;
        Debug.Log(PlayerStats.sport);
        Debug.Log(PlayerStats.level);
        Debug.Log(PlayerStats.competing_years);
        Debug.Log(PlayerStats.height);
        Debug.Log(PlayerStats.weight);
    }

    //STORE INFORMATION!
}