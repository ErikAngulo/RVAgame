using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    
    private string _escena_principal = "MainMenuScene";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)){
            if (SceneManager.GetActiveScene().name.Equals(_escena_principal)){
                ExitGame();
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

}
