using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseEvent : BaseEvent
{
    [SerializeField]
    int chance;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "";

        int aux = Random.Range(0, 100);

        if (aux > chance)
        {
            button.onClick.AddListener(functionMinusLouse);
        }
        else
        {
            button.onClick.AddListener(functionMinusHair);
        }
    }

    // Update is called once per frame
    void functionMinusLouse()
    {
        GameManager.instance.louseAccumulated -= 3;
        text.text = "";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }

    void functionMinusHair()
    {
        GameManager.instance.hairsCollected -= 3;
        text.text = "";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }
}
