using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;
    public Settings optionsLoader;
    public LevelLoader levelLoader;
    public PauseMenu pauseMenu;

    int hairsCollected;
    int louseAccumulated;

    enum Difficulty { Bald, Normal, Hippie };

    Difficulty currentDifficulty = Difficulty.Normal;
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
            optionsLoader = GetComponent<Settings>();
            levelLoader = GetComponentInChildren<LevelLoader>();
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Rewired.ReInput.players.Players[0].GetButtonDown("ButtonStart"))
        {
            pauseMenu.TogglePause();
        }
    }

    void changeCurrentDifficulty(Difficulty difficulty)
    {
        currentDifficulty = difficulty;
    }

    void difficultyRight()
    {
        if (currentDifficulty == Difficulty.Hippie)
            currentDifficulty = Difficulty.Bald;
        else
            currentDifficulty++;
    }

    void difficultyLeft()
    {
        if (currentDifficulty == Difficulty.Bald)
            currentDifficulty = Difficulty.Hippie;
        else
            currentDifficulty--;
    }

    public void GoToPlayScene(int biome)
    {
        biome_ = biome;
        SceneManager.LoadScene("ZonaScene");   
    }

    public int GetBiome()
    {
        return biome_;
    }

    public void AddHair(int cantHair, int cantPiojos)
    {
        hairsCollected += cantHair;
        louseAccumulated += cantPiojos;
    }

    private int biome_ = 0;
}
