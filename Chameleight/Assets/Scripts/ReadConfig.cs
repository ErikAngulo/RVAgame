using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReadConfig : MonoBehaviour
{
    // Start is called before the first frame update

    public TimeController timeController;
    public HoldController holdController;
    public ButtonController buttonController;
    public gameController gamecontroller;

    private string _ball_game = "MainScene";
    private string _shooting_game = "Chameleight_Scenary";

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals(_ball_game)){
            timeController.totalTime = StaticClass.Time;
            holdController.ballSpeed = StaticClass.BallSpeed;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_shooting_game)){
            gamecontroller.movement = StaticClass.TargetMovement;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
