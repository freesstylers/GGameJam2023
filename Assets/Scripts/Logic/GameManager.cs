using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;
    public Settings optionsLoader;

    int hairsCollected;
    int louseAccumulated;

    enum Difficulty { Bald, Normal, Hippie};

    Difficulty currentDifficulty = Difficulty.Normal;
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
            optionsLoader = GetComponent<Settings>();
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
}
