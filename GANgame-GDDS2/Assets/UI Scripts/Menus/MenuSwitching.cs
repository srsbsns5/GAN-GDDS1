using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitching : MonoBehaviour
{
    AudioManager audioManager;

    public void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("House1");
        
        
    }

    public void LoadSaves()
    {
        SceneManager.LoadScene("LoadGame");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
        
       
        
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
        
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
        
    }

}