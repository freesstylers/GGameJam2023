using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEvent : MonoBehaviour
{
    public Button button;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update

    public void activateText()
    {
        text.gameObject.SetActive(true);
    }

    public void deactivateText()
    {
        text.gameObject.SetActive(false);
    }

    public void changeText(string text_)
    {
        text.text = text_;
    }
}
