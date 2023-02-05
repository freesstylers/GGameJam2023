using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetEvent : BaseEvent
{
    [SerializeField]
    int chance;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        text.text = "Wanna try your luck? Just give me a hand. Win or lose, it is fun all the same!";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(bet);
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Okay, let's bet!";
    }

    void bet()
    {
        int aux = Random.Range(0, 100);

        if (aux > chance) //Louse
        {
            aux = Random.Range(0, 100);

            button.onClick.RemoveAllListeners();
            text.text = "Ok, we are gonna play with louse";
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
            text.text = "Ok, we are gonna play with hair";

            if (aux > chance)
            {
                button.onClick.AddListener(functionMoreHair);
            }
            else
            {
                button.onClick.AddListener(functionMinusHair);
            }
        }

        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Fine, do it";
    }
    // Update is called once per frame
    void functionMinusLouse()
    {
        int aux = Random.Range(0, GameManager.instance.louseAccumulated);

        GameManager.instance.louseAccumulated -= aux;
        text.text = "You have lost " + aux.ToString() + " Lice to the Shampoo. But now you will smell Minty Fresh";

        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Live Lice free, I guess";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            base.deactivateText();
            NextState();
        });
    }

    void functionMinusHair()
    {

        int aux = Random.Range(0, GameManager.instance.hairsCollected);

        GameManager.instance.louseAccumulated -= aux;
        text.text = "I will take " + aux.ToString() + " pieces of hair. AND STOP TRYING TO TAKE MORE";

        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "****";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            base.deactivateText();
            NextState();
        });
    }

    void functionMoreLouse()
    {
        int aux = Random.Range(0, 5);

        GameManager.instance.louseAccumulated += aux;
        text.text = "Congrats! You earned " + aux.ToString() + " more Lice. Are you trying to invade a country?";
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "No. I am invading your scalp";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => 
        {
            base.deactivateText();
            NextState();
        }); 
    }

    void functionMoreHair()
    {
        int aux = Random.Range(0, 5);

        GameManager.instance.hairsCollected += aux;
        text.text = "Congratulations! You are " + aux.ToString() + " hairs further from being bald";
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Wear your hair with pride";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            base.deactivateText();
            NextState();
        });
    }

    private void NextState()
    {
        GameManager.instance.levelLoader.LoadTransition(States.RutaState);
        Destroy(button);
        Destroy(gameObject);
    }
}
