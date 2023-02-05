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
        text.text = "";

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

    // Update is called once per frame
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
