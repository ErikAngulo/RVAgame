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
                "Ball game objective is to throw balls to specific color area."
                + System.Environment.NewLine +
                "There are two possible ball colors, red and blue."
                + System.Environment.NewLine +
                "When blue ball appears, throw the ball to the blue square area located at the panel."
                + System.Environment.NewLine +
                "When red ball appears, throw the ball to the red square area located at the panel."
                + System.Environment.NewLine +
                "You have a single opportunity per ball to hit the target."
                + System.Environment.NewLine +
                "After each throw, a new ball will appear in front of you."
                + System.Environment.NewLine +
                "Test ends after throwing the last ball."
                + System.Environment.NewLine;
            }
            else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
                _text.text = "" +
                "Shooting game objective is to shoot at the center of a target."
                + System.Environment.NewLine +
                "There are two light bulbs on top of two targets. One bulb is blue, and the other is orange."
                + System.Environment.NewLine +
                "When blue bulb lights  turns on, shoot to the target located below."
                + System.Environment.NewLine +
                "When the orange light turns on, shoot to the target located below."
                + System.Environment.NewLine +
                "The light bulb remains on until a bullet lands on the correct target.  "
                + System.Environment.NewLine +
                "After that, a new light bulb turns on."
                + System.Environment.NewLine +
                "Highest score is at the center of the target, whereas the lowest at the target edge."
                + System.Environment.NewLine +
                "You have a time limit to score  as many points as possible."
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
                "There are two options to perform the test: Throw with a controller or you hand."
                + System.Environment.NewLine +
                "Playing with the controller requires to pick the ball with the hand trigger."
                + System.Environment.NewLine +
                "Playing with your hand requires leaving the controller on a table at the start of the game."
                + System.Environment.NewLine +
                "Check the video on the side for an execution example."
                + System.Environment.NewLine +
                "You can exit the game any time pressing X button (left controller)."
                + System.Environment.NewLine;

                _leftText.text = "Using hands";
                _leftVideo.clip = (VideoClip)Resources.Load("BallHand");
                _rightText.text = "Using controller";
                _rightVideo.clip = (VideoClip)Resources.Load("BallController");

            }
            else if (StaticClass.SelectedGameScene.Equals(_shooting_game)){
                _text.text = "" +
                "You have a controller to perform the test."
                + System.Environment.NewLine +
                "In the next scene, you can select the right or left controller."
                + System.Environment.NewLine +
                "The controller represents the gun and its movement."
                + System.Environment.NewLine +
                "Press the Index trigger to shoot bullets."
                + System.Environment.NewLine +
                "Check the video on the side for an execution example."
                + System.Environment.NewLine +
                "You can exit the game any time pressing X button (left controller)."
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
