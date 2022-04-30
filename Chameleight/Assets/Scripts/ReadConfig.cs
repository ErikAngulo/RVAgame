using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ReadConfig : MonoBehaviour
{
    // Start is called before the first frame update

    public TimeController timeController;
    public HoldController holdController;
    public ButtonController buttonController;
    public gameController gamecontroller;
    public TextMeshProUGUI text;

    private string _ball_game = "MainScene";
    private string _shooting_game = "Chameleight_Scenary";
    private string _score_scene = "GameOverScene";

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals(_ball_game)){
            holdController.totalLimit = StaticClass.BallLimit;
            holdController.ballSpeed = StaticClass.BallSpeed;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_shooting_game)){
            gamecontroller.movement = StaticClass.TargetMovement;
            gamecontroller.playTime = StaticClass.Time;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_score_scene)){
            text = GameObject.Find("ResultsText").GetComponent<TextMeshProUGUI>();
            text.text = StaticClass.scoreText;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
