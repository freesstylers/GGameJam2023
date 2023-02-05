using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { RutaState, GameState, EventState };
public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public States currentState;
    public List<GameObject> objectStates;
    public EnemySpawner enemySpawner;

    private void Start()
    {
        instance = this;
        ChangeState(States.RutaState);        
    }

    public void ChangeState(States s)
    {
        ClearStates();
        objectStates[(int)s].SetActive(true);
        currentState = s;

        switch (s)
        {
            case States.GameState:
                GameManager.instance.SetBiome(Random.Range(0, 4));
                MusicManager.instance.PlayLevelMusic();
                break;
            case States.EventState:
                enemySpawner.EraseEnemies();
                MusicManager.instance.PlayEventMusic();
                break;
            case States.RutaState:
                MusicManager.instance.PlayLevelSelectionMusic();
                break;
            default:
                break;
        }
    }

    private void ClearStates()
    {
        objectStates.ForEach(x => x.SetActive(false));
    }
}
