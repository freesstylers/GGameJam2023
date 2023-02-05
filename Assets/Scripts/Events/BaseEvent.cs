using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseEvent : MonoBehaviour
{
    public Button button;
    public TMPro.TextMeshProUGUI text;

    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }

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
