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
        text.text = "Hygiene police. Show us what you got";
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Ok... I did nothing wrong";
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

    private void NextState()
    {
        GameManager.instance.levelLoader.LoadTransition(States.RutaState);
        Destroy(button);
        Destroy(gameObject);
    }
}
