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

    private PlayerInput player;
    public int hairsCollected;
    public int louseAccumulated;

    enum Difficulty { Bald, Normal, Hippie };
    public enum States { MenuState, GameState, EventState, RutaState, ResumeAfterG };
    public States currentState;
    public List<GameObject> objectStates;

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

        //player = GameObject.Find("Player").GetComponent<PlayerInput>();
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
        ChangeState(States.RutaState);
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

    public void EndLevel()
    {
        //player.EndLevel();
        ChangeState(States.ResumeAfterG);
    }

    public void ChangeState(States s)
    {
        ClearStates();
        objectStates[(int)s].SetActive(true);
        currentState = s;

        switch (s)
        {
            case States.MenuState:
                
                break;
            case States.GameState:
                break;
            case States.EventState:
                break;
            case States.RutaState:
                break;
            case States.ResumeAfterG:
                break;
            default:
                break;
        }
    }

    private void ClearStates()
    {
        objectStates.ForEach(x => x.SetActive(false));
    }

    private int biome_ = 0;
}
