using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{

    public void SetPosition(float ypos)
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, ypos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Aqui deberia terminar el nivel
        Debug.Log("Finito");
        GameManager.instance.EndLevel();
    }
}
