using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MainMenu : MonoBehaviour
{
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
    }

    public void HighScores()
    {

    }
}
