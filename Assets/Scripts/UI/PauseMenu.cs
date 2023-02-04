using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject settingsMenuGameObject;
    [SerializeField]
    GameObject containerGameObject;

    bool settingsOpened = false;

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void BackToGame()
    {
        //Unpause
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        GameManager.instance.levelLoader.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingsOpened = !settingsOpened;

        if (settingsOpened)
        {
            containerGameObject.SetActive(false);
            settingsMenuGameObject.SetActive(true);
        }
        else
        {
            containerGameObject.SetActive(true);
            settingsMenuGameObject.SetActive(false);
        }
    }
}
