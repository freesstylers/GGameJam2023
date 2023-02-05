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
        text.text = "Wanna try your luck? Just give me a hand. Win or lose, it is fun all the same!";

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
        text.text = "R.I.P. Bozo";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }

    void functionMinusHair()
    {
        GameManager.instance.hairsCollected -= 3;
        text.text = "Too much H&S";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }

    void functionMoreLouse()
    {
        GameManager.instance.louseAccumulated += 3;
        text.text = "Congrats! You stink";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }

    void functionMoreHair()
    {
        GameManager.instance.hairsCollected += 3;
        text.text = "Puberty!";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(base.deactivateText);
    }
}
