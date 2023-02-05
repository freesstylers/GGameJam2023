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

        PlayerInput p = collision.gameObject.GetComponent<PlayerInput>();

        if (p != null)
        {
            //Aqui deberia terminar el nivel
            Debug.Log("Finito");

            p.EndLevel();
        }
        //Aqui deberia terminar el nivel
        Debug.Log("Finito");
    }
}
