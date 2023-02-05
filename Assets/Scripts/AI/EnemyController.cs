using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float HP;

    public void Damage(float dmg)
    {
        HP -= dmg;

        if(HP <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
