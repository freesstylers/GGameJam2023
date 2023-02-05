using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States { RutaState, GameState, EventState };
public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    public States currentState;
    public List<GameObject> objectStates;

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
                break;
            case States.EventState:
                break;
            case States.RutaState:
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
