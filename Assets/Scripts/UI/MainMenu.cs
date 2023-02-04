using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject containerGameObject;
    [SerializeField]
    GameObject settingsGameObject;
    [SerializeField]
    GameObject creditsGameObject;

    bool settingsOpened = false;
    bool creditsOpened = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/FreeStylers_Dev");
    }

    public void ItchIO()
    {
        Application.OpenURL("https://freestylers-studio.itch.io/");
    }

    public void GlobalGameJam()
    {
        Application.OpenURL("https://globalgamejam.org/");
    }

    public void PlayButton()
    {
    }

    public void Settings()
    {
        settingsOpened = !settingsOpened;

        if (settingsOpened)
        {
            containerGameObject.SetActive(false);
            settingsGameObject.SetActive(true);
        }
        else
        {
            containerGameObject.SetActive(true);
            settingsGameObject.SetActive(false);
        }
    }

    public void HighScores()
    {

    }

    public void Credits()
    {
        creditsOpened = !creditsOpened;

        if (creditsOpened)
        {
            containerGameObject.SetActive(false);
            creditsGameObject.SetActive(true);
        }
        else
        {
            containerGameObject.SetActive(true);
            creditsGameObject.SetActive(false);
        }
    }
}
