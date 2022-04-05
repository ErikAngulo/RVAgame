using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (SceneManager.GetActiveScene().name.Equals(_escena_principal)){
                Application.Quit();
            }
            else{
                SceneManager.LoadScene(_escena_principal);
            }
        }
    }

    public void ChangeScene (string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ExitGame(){
        Application.Quit();
    }

}
