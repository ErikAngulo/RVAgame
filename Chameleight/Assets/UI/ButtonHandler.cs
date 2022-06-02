using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    
    // Class to hand menu UI buttons, scene change and exiting

    private string _escena_principal = "MainMenuScene";
    private List<string> logOutScenes = new List<string>();
    OVRCameraRig _cameraRig;
    OVRInputModule _inputModule;

    // Start is called before the first frame update
    void Start()
    {
        logOutScenes.Add("WelcomeScene");
        logOutScenes.Add("Initial Questions");
        logOutScenes.Add("Initial Questions 2");
        logOutScenes.Add("LoginScene");

        _cameraRig = FindObjectOfType<OVRCameraRig>();
        _inputModule = FindObjectOfType<OVRInputModule>();
    }

    // Update is called once per frame
    void Update()
    {
        // Exit with controller X button

        //Exiting game, play or scene. With controller X button. Only if any controller active (deactivate if playing with hand tracking)
        if ((OVRInput.GetActiveController() == OVRInput.Controller.Touch || OVRInput.GetActiveController() == OVRInput.Controller.LTouch 
        || OVRInput.GetActiveController() == OVRInput.Controller.RTouch) && OVRInput.GetDown(OVRInput.Button.Three)){ // X button
            if (SceneManager.GetActiveScene().name.Equals(_escena_principal)){
                //Exit game if we are at main scene
                ExitGame();
            }
            else if (logOutScenes.Contains(SceneManager.GetActiveScene().name)){
                //Log out if we are logged or creating user
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("WelcomeScene");
            }
            else{
                // Return to main menu scene
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(_escena_principal);
            }
        }
    }
    // Change scene (only to any menu scene, in order to play or continue background music)
    public void ChangeScene (string scene)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MusicManager.instance.play();
        SceneManager.LoadScene(scene);
    }

    // Save selected game scene name
    public void SelectedGameScene (string name){
        StaticClass.SelectedGameScene = name;
    }

    // Go to selected game scene, stopping menu music
    public void GoToSelectedGameScene(){
        MusicManager.instance.stop();
        SceneManager.LoadScene(StaticClass.SelectedGameScene);
    }

    // Close game completely
    public void ExitGame(){
        Application.Quit();
    }

    // Save user ID
    public void SetCurrentPlayerId(string id){
        StaticClass.playerId = id;
    }

}
