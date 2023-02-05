using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEvent : BaseEvent
{
    // Start is called before the first frame update
    void Start()
    {
        MusicManager.instance.PlayShopMusic();
    }

    public void TryBuySpeed()
    {
        //Check money
        if (GameManager.instance.hairsCollected >= 15)
        {
            GameManager.instance.hairsCollected -= 15;
            GameManager.instance.player.Speed++;
            NextState();
            //Modify player stats
        }
        else
        {
            //Sound
            //Modify color
        }
    }

    public void TryBuyDamage()
    {
        //Check money
        if (GameManager.instance.hairsCollected >= 15)
        {
            GameManager.instance.hairsCollected -= 15;
            GameManager.instance.player._attackArea.damage++;
            NextState();
            //Modify player stats
        }
        else
        {
            //Sound
            //Modify color
        }
    }

    public void TryBuyHealth()
    {
        //Check money
        if (GameManager.instance.hairsCollected >= 15)
        {
            GameManager.instance.hairsCollected -= 15;
            GameManager.instance.player.MAX_HP++;
            NextState();
            //Modify player stats
        }
        else
        {
            //Sound
            //Modify color
        }
    }

    public void Pass()
    {
        NextState();
    }

    private void NextState()
    {
        GameManager.instance.levelLoader.LoadTransition(States.RutaState);
        Destroy(button);
        Destroy(gameObject);
    }
}
