using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DoDamage(collision);
        }
    }

    public virtual void DoDamage(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerInput>().TakeDamage(Damage);
    }
}
