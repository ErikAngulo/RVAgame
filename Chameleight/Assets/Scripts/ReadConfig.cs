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

    private string _test_game = "MainScene";

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals(_test_game)){
            timeController.totalTime = float.Parse(StaticClass.Time);
            holdController.ballSpeed = float.Parse(StaticClass.BallSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
