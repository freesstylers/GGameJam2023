using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetEvent : BaseEvent
{
    [SerializeField]
    int chance;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(bet);
    }

    void bet()
    {
        int aux = Random.Range(0, 100);

        if (aux > chance) //Louse
        {
            aux = Random.Range(0, 100);

            button.onClick.RemoveAllListeners();

            if (aux > chance)
            {
                button.onClick.AddListener(functionMoreLouse);
            }
            else
            {
                button.onClick.AddListener(functionMinusLouse);
            }
        }
        else //Hair
        {
            aux = Random.Range(0, 100);

            button.onClick.RemoveAllListeners();

            if (aux > chance)
            {
                button.onClick.AddListener(functionMoreHair);
            }
            else
            {
                button.onClick.AddListener(functionMinusHair);
            }
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

    void functionMoreLouse()
    {
        GameManager.instance.louseAccumulated += 3;
        text.text = "";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }

    void functionMoreHair()
    {
        GameManager.instance.hairsCollected += 3;
        text.text = "";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }
}
