using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampuController : MonoBehaviour
{
    //Tiempo que tardara el champu el llenar la pantalla en segundos
    private float timeToFill_ = 120.0f;
    //Tamaño vertical de la zona
    private float mapSize;

    private bool canMove = true;

    void Update()
    {
        if (canMove)
        {
            float deltaTime = Time.deltaTime;

            Vector2 newpos = new Vector2(transform.position.x, transform.position.y);

            newpos.y += (mapSize / timeToFill_) * deltaTime;

            this.transform.position = newpos;
        }
    }

    public void SetMapSize(float size)
    {
        mapSize = size;
    }

    public void SetTimeToFill(float time)
    {
        timeToFill_ = time;
    }

    public void SetStartingPos(Vector2 newpos)
    {
        transform.position = newpos;
    }

    public void EndLevel()
    {
        canMove = false;
    }
}
