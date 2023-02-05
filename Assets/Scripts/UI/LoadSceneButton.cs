using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{

    public void Load(string s)
    {
        GameManager.instance.louseAccumulated = 0;
        GameManager.instance.hairsCollected = 0;

        GameManager.instance.levelLoader.LoadScene(s);
    }
}
