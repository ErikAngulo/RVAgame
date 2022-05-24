using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour
{
    
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
        if (OVRInput.GetDown(OVRInput.Button.Three)){ // X button
            if (SceneManager.GetActiveScene().name.Equals(_escena_principal)){
                ExitGame();
            }
            else if (logOutScenes.Contains(SceneManager.GetActiveScene().name)){
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("WelcomeScene");
            }
            else{
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(_escena_principal);
            }
        }
    }
    public void ChangeScene (string scene)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(scene);
    }

    public void SelectedGameScene (string name){
        StaticClass.SelectedGameScene = name;
    }

    public void GoToSelectedGameScene(){
        ChangeScene(StaticClass.SelectedGameScene);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void SetCurrentPlayerId(string id){
        StaticClass.playerId = id;
    }

}
