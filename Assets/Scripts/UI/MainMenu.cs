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

    // Update is called once per frame
    void Update()
    {
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonA"))
        {
            Debug.Log("Input");
        }
    }
}
