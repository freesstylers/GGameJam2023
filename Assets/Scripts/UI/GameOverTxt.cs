using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverTxt : MonoBehaviour
{
    public TextMeshProUGUI _hairs;
    public TextMeshProUGUI _pipis;

    // Start is called before the first frame update
    void Awake()
    {
        _hairs.text = GameManager.instance.hairsCollected.ToString();
        _pipis.text = GameManager.instance.louseAccumulated.ToString();
    }
}
