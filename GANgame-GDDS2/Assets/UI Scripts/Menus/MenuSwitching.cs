using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitching : MonoBehaviour
{
    public string playScene;
    public string loadGame;
    public string creditsScene;
    public string mainMenuScene;

    public void Start()
    {
       

    }
    public void PlayGame()
    {
        SceneManager.LoadScene(playScene);


    }

    public void LoadSaves()
    {
        SceneManager.LoadScene("LoadGame");
    }

    

    public void Credits()
    {
        SceneManager.LoadScene("Credits");

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
