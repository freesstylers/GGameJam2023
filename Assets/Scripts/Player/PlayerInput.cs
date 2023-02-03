using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
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
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonB"))
        {

        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonX"))
        {

        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonY"))
        {

        }
        if (Rewired.ReInput.players.Players[0].GetButton("ButtonStart"))
        {

        }
        if (Rewired.ReInput.players.Players[0].GetAxis("YAxis") != 0.0f)
        {
            Debug.Log("Axis Y");
        }
        if (Rewired.ReInput.players.Players[0].GetAxis("XAxis") != 0.0f)
        {
            Debug.Log("Axis X");
        }
        
    }
}
