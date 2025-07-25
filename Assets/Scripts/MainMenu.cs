using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject startButton;
    public GameObject endButton;
    public String sceneName;
    public bool gameStarted = false;

    void Update()
    {
        if (gameStarted == true)
        {
            menu.SetActive(false);
            startButton.SetActive(false);
            endButton.SetActive(true);

            SceneManager.LoadScene(sceneName);
        }
    }

    public void StartGame()
    {
        gameStarted = true;
    }
}
