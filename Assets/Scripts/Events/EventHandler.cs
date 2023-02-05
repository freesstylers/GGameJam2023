using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    private BaseEvent evento;

    private void OnEnable()
    {
        Instantiate(evento);        
    }

    public void SetEvento(BaseEvent e)
    {
        evento = e;
    }
}
