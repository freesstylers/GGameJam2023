using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedScript : MonoBehaviour
{
    public Sprite[] sprites_;

    public void setSprite(int index)
    {
        GetComponent<SpriteRenderer>().sprite = sprites_[index];
    }
}
