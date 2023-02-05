using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinEvent : BaseEvent
{
    // Start is called before the first frame update
    void Start()
    {
        text.text = "You did it! This head is yours! Now, onto the next....";
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Yippee!";
        button.onClick.AddListener(End);
    }

    void End()
    {
        GameManager.instance.defeat = false;
        GameManager.instance.levelLoader.LoadScene("GameOverScreen");
    }
}
