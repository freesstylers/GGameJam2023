using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject settingsMenuGameObject;
    [SerializeField]
    GameObject containerGameObject;
    
    [SerializeField]
    GameObject continueButton;

    bool settingsOpened = false;
    bool containerOpened = false;

    public void TogglePause()
    {
        if (settingsOpened)
            BackToGame();
        else
            Pause();
    }

    public void Pause()
    {
        if (SceneController.instance.currentState == States.GameState)
        {
            Time.timeScale = 0;

            Container();

            EventSystem.current.SetSelectedGameObject(continueButton);
        }
    }

    public void BackToGame()
    {
        //Unpause
        Time.timeScale = 1;

        Container();
    }

    public void ToMainMenu()
    {
        GameManager.instance.levelLoader.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Container()
    {
        containerOpened = !containerOpened;

        if (containerOpened)
        {
            containerGameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(continueButton);
        }
        else
        {
            containerGameObject.SetActive(false);
        }
    }

    public void Settings()
    {
        settingsOpened = !settingsOpened;

        if (settingsOpened)
        {
            containerGameObject.SetActive(false);
            settingsMenuGameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(settingsMenuGameObject.GetComponentInChildren<Button>().gameObject);
        }
        else
        {
            containerGameObject.SetActive(true);
            settingsMenuGameObject.SetActive(false);
        }
    }
}
