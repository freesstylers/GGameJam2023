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

    int hairsCollected;
    int louseAccumulated;

    enum Difficulty { Bald, Normal, Hippie };

    Difficulty currentDifficulty = Difficulty.Normal;
    void Start()
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

    private int biome_ = 0;
}
