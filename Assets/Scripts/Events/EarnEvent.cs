using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarnEvent : BaseEvent
{
    [SerializeField]
    int chance;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        text.text = "You have earned a Dogecoin. It has no value, so I will exchange it for something more useful for you";
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Ok... Thankfully I am not Elon Musk";

        int aux = Random.Range(0, 100);

        if (aux > chance)
        {
            button.onClick.AddListener(functionMoreLouse);
        }
        else
        {
            button.onClick.AddListener(functionMoreHair);
        }
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
