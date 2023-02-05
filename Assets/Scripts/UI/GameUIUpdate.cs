using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIUpdate : MonoBehaviour
{
    public TextMeshProUGUI _pipis;
    public TextMeshProUGUI _pelos;
    public TextMeshProUGUI _HP;

    private void Update()
    {
        _pipis.text = GameManager.instance.louseAccumulated.ToString();
        _pelos.text = GameManager.instance.hairsCollected.ToString();


        _HP.text = (GameManager.instance.player.HP.ToString() + "/" + GameManager.instance.player.MAX_HP.ToString());
    }
}
