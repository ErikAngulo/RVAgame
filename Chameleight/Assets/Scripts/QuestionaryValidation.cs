using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Globalization;

public static class PlayerInfo
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
    public IOController iOController;
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
    private string _main_scene = "MainMenuScene";
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
        if(Directory.Exists("../Database/"+email.text)){
            warning.text = "A user with the specified email already exists.";
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

        PlayerInfo.player_name = player_name.text;
        PlayerInfo.email = email.text;
        PlayerInfo.birthday = dateVal;
        IEnumerator<Toggle> toggles = gender.ActiveToggles().GetEnumerator();
        toggles.MoveNext();
        PlayerInfo.gender = toggles.Current.GetComponentInChildren<Text>().text;
        toggles = laterality.ActiveToggles().GetEnumerator();
        toggles.MoveNext();
        PlayerInfo.laterality = toggles.Current.GetComponentInChildren<Text>().text;
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

        int age = today.Year - PlayerInfo.birthday.Year;

        if (PlayerInfo.birthday > today.AddYears(-age)) age--;

        if(int.Parse(competing_years.text)>age){
            warning.text = "Number of competing years must be less than or equal to your age.";
            return;
        }

        warning.text = "";

        PlayerInfo.sport = sport.options[sport.value].text;
        IEnumerator<Toggle> toggles = level.ActiveToggles().GetEnumerator();
        toggles.MoveNext();
        PlayerInfo.level = toggles.Current.GetComponentInChildren<Text>().text;
        PlayerInfo.competing_years = int.Parse(competing_years.text);
        PlayerInfo.height = (int) height.value;
        PlayerInfo.weight = (int) weight.value;

        iOController.RegisterUser();
        StaticClass.playerId = PlayerInfo.email;
        buttonHandler.ChangeScene(_main_scene);
    }

    public void Login(){
        if(!IsValidEmailAddress(email.text)){
            warning.text = "Please, enter a valid email address.";
            return;
        }
        if(!Directory.Exists("../Database/"+email.text)){
            warning.text = "Incorrect credentials. Please, enter an already registered email.";
            return;
        }
        warning.text = "";
        StaticClass.playerId = email.text;
        buttonHandler.ChangeScene(_main_scene);
    }
}