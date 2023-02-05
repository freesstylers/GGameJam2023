using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameOverTxt : MonoBehaviour
{
    public TextMeshProUGUI _hairs;
    public TextMeshProUGUI _pipis;

    [SerializeField]
    Button backToMenu;
    // Start is called before the first frame update
    void Awake()
    {
        _hairs.text = GameManager.instance.hairsCollected.ToString();
        _pipis.text = GameManager.instance.louseAccumulated.ToString();

        EventSystem.current.SetSelectedGameObject(backToMenu.gameObject);
    }
}
