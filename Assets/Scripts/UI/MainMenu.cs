using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject containerGameObject;
    [SerializeField]
    GameObject settingsGameObject;
    [SerializeField]
    GameObject creditsGameObject;

    GameObject selected;
    [SerializeField]
    GameObject firstSelected;

    bool settingsOpened = false;
    bool creditsOpened = false;
    bool playing = false;

    // Start is called before the first frame update
    void Start()
    {
        settingsGameObject.SetActive(settingsOpened);
        creditsGameObject.SetActive(creditsOpened);
        EventSystem.current.SetSelectedGameObject(firstSelected);
        playing = false;
    }

    public void Twitter()
    {
        if(playing)
            return;

        Application.OpenURL("https://twitter.com/FreeStylers_Dev");
    }

    public void GGJ()
    {
        if(playing)
            return;

        Application.OpenURL("https://www.gamejam.es");
    }

    public void ItchIO()
    {
        if(playing)
            return;

        Application.OpenURL("https://freestylers-studio.itch.io/");
    }

    public void GlobalGameJam()
    {
        if (playing)
            return;

        Application.OpenURL("https://globalgamejam.org/");
    }

    public void PlayButton(Button b)
    {
        if (playing)
            return;

        playing = true;
        GameManager.instance.levelLoader.LoadScene("GameScreen");
    }

    public void Settings()
    {
        if(playing)
            return;

        settingsOpened = !settingsOpened;

        if (settingsOpened)
        {
            selected = EventSystem.current.currentSelectedGameObject;

            containerGameObject.SetActive(false);
            settingsGameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(settingsGameObject.GetComponentInChildren<Button>().gameObject);
        }
        else
        {
            containerGameObject.SetActive(true);
            settingsGameObject.SetActive(false);

            EventSystem.current.SetSelectedGameObject(selected);
        }
    }

    public void HighScores()
    {
        if(playing)
            return;


    }

    public void Credits()
    {
        if(playing)
            return;

        creditsOpened = !creditsOpened;

        if (creditsOpened)
        {
            selected = EventSystem.current.currentSelectedGameObject;

            containerGameObject.SetActive(false);
            creditsGameObject.SetActive(true);

            EventSystem.current.SetSelectedGameObject(creditsGameObject.GetComponentInChildren<Button>().gameObject);
        }
        else
        {
            containerGameObject.SetActive(true);
            creditsGameObject.SetActive(false);

            EventSystem.current.SetSelectedGameObject(selected);
        }
    }
}
