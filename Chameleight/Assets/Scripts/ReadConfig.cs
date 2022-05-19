using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;
using UnityEngine.UI;

public class ReadConfig : MonoBehaviour
{
    // Start is called before the first frame update

    public TimeController timeController;
    public HoldController holdController;
    public ButtonController buttonController;
    public gameController gamecontroller;
    private TextMeshProUGUI _text;
    private TextMeshProUGUI _leftText;
    private TextMeshProUGUI _rightText;
    private VideoPlayer _leftVideo;
    private VideoPlayer _rightVideo;
    private string _ball_game = "MainScene";
    private string _shooting_game = "Chameleight_Scenary";
    private string _score_scene = "GameOverScene";
    private string _instruction_scene = "InstructionsScene";
    private string _controller_scene = "ControllerInstructScene";

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
            _text = GameObject.Find("ResultsText").GetComponent<TextMeshProUGUI>();
            _text.text = StaticClass.scoreText;
        }
        else if (SceneManager.GetActiveScene().name.Equals(_instruction_scene)){
            _text = GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>();
            if (StaticClass.SelectedGameScene.Equals(_ball_game)){
                _text.text = "" +
                "A red or blue ball will appear at the floor."
                + System.Environment.NewLine +
                "You must throw it to the button of the same color as the ball."
                + System.Environment.NewLine +
                "Once throwed, another ball will appear to throw."
                + System.Environment.NewLine +
                "This steps must be followed until all balls are used."
                + System.Environment.NewLine +
                "In the configuration screen, you can select how many balls will be available to throw."
                + System.Environment.NewLine +
                "When the game finishes, game statistics will appear and you will have the option to play again if wished."
                + System.Environment.NewLine;
            }
            else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
                _text.text = "" +
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
                "In the configuration screen, you can select how many time you want to play."
                + System.Environment.NewLine +
                "Besides, a 'movement' option can be selected with 'Yes'."
                + System.Environment.NewLine +
                "This means that the targets will move after it is hit."
                + System.Environment.NewLine +
                "When the game finishes, game statistics will appear and you will have the option to play again if wished."
                + System.Environment.NewLine;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals(_controller_scene)){
            _text = GameObject.Find("InstructionText").GetComponent<TextMeshProUGUI>();
            _leftText = GameObject.Find("LeftText").GetComponent<TextMeshProUGUI>();
            _rightText = GameObject.Find("RightText").GetComponent<TextMeshProUGUI>();
            _leftVideo = GameObject.Find("LeftVideo").GetComponent<VideoPlayer>();
            _rightVideo = GameObject.Find("RightVideo").GetComponent<VideoPlayer>();

            if (StaticClass.SelectedGameScene.Equals(_ball_game)){
                _text.text = "" +
                "You can play with both controllers."
                + System.Environment.NewLine +
                "Pick the ball with hand trigger."
                + System.Environment.NewLine +
                "You can also play using your hands."
                + System.Environment.NewLine +
                "To do so, leave the controllers in a table when the game starts."
                + System.Environment.NewLine +
                "Check the videos next to this text to know the gestures to throw the ball."
                + System.Environment.NewLine;

                _leftText.text = "Using hands";
                _leftVideo.clip = (VideoClip)Resources.Load("BallHand");
                _rightText.text = "Using controller";
                _rightVideo.clip = (VideoClip)Resources.Load("BallController");

            }
            else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
                _text.text = "" +
                "You will play with the controller you select in the next screen."
                + System.Environment.NewLine +
                "You can move in the platform with the joystick of the left controller."
                + System.Environment.NewLine +
                "Move the controller to move the gun."
                + System.Environment.NewLine +
                "When pressing index trigger you will shoot the gun."
                + System.Environment.NewLine;

                _rightText.text = "Using controller";
                _rightVideo.clip = (VideoClip)Resources.Load("ShootingController");
                _leftVideo.enabled = false;
                _leftText.text = "";
                GameObject.Find("LeftImage").GetComponent<RawImage>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
