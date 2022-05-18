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
    private string _instruction_scene = "InstructionsScene";

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals(_ball_game)){
            holdController.totalLimit = StaticClass.BallLimit;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_shooting_game)){
            gamecontroller.movement = StaticClass.TargetMovement;
            gamecontroller.playTime = StaticClass.Time;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_score_scene)){
            text = GameObject.Find("ResultsText").GetComponent<TextMeshProUGUI>();
            text.text = StaticClass.scoreText;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_instruction_scene)){
            text = GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>();
            if (StaticClass.SelectedGameScene.Equals(_ball_game)){
                text.text = "" +
                "A red or blue ball will appear at the floor."
                + System.Environment.NewLine +
                "You must throw it to the button of the same color as the ball."
                + System.Environment.NewLine +
                "Once throwed, another ball will appear to throw."
                + System.Environment.NewLine +
                "This steps must be followed until all balls are used."
                + System.Environment.NewLine +
                "In the next screen, you can select how many balls will be available to throw."
                + System.Environment.NewLine +
                "When the game finishes, game statistics will appear and you will have the option to play again if wished."
                + System.Environment.NewLine;
            }
            else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
                text.text = "" +
                "A gun will appear, which you will use to shoot."
                + System.Environment.NewLine +
                "You must shoot to the target that has a light on."
                + System.Environment.NewLine +
                "You have unlimited shoots to hit it."
                + System.Environment.NewLine +
                "Once hit, the bulb will turn off and the points scored will be shown."
                + System.Environment.NewLine +
                "After a period of time, a bulb of a target will turn on and you must repeat the process."
                + System.Environment.NewLine +
                "In the next screen, you can select how many time you want to play."
                + System.Environment.NewLine +
                "Besides, a 'movement' option can be selected with 'Yes'."
                + System.Environment.NewLine +
                "This means that the targets will move after it is hit."
                + System.Environment.NewLine +
                "When the game finishes, game statistics will appear and you will have the option to play again if wished."
                + System.Environment.NewLine;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
