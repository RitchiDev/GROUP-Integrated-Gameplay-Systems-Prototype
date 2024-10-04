using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu
{
    public event Action SceneChange;

    public Menu()
    {
        SceneManager.activeSceneChanged += SceneChanged;
        SetupButtons();
    }

    private void SetupButtons()
    {
        //CanvasBuilder doesn't exist in this scene...
        GameObject startButton = GameObject.Find("Start Button");
        GameObject quitButton = GameObject.Find("Quit Button");

        startButton.GetComponent<Button>().onClick.AddListener(StartGame);
        quitButton.GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene((int)SceneIndexes.GAME);
    }
    
    private void QuitGame()
    {
        Application.Quit();
    }

    private void SceneChanged(Scene _old, Scene _new)
    {
        if (_new.buildIndex != (int)SceneIndexes.MENU)
        {
            SceneChange?.Invoke();
            SceneChange = null;
            SceneManager.activeSceneChanged -= SceneChanged;
        }
    }
}
