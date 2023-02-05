using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    bool DEBUG_ATTACK = false;

    PlayerInput _player;
    public float damage;
    public bool attacking = false;

    public void Initialize(PlayerInput p)
    {
        _player = p;
    }

    List<EnemyController> inRange = new List<EnemyController>();
    List<EnemyController> attacked = new List<EnemyController>();
    List<EnemyController> dead = new List<EnemyController>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            // Add to set of characters that are in range of attack
            inRange.Add(other.GetComponent<EnemyController>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            // Remove, since it exit the range of the attacking character.
            inRange.Remove(enemy);
        }
    }

    // Call this function whenever you want to do damage to all characters in range.
    public void Attack()
    {
        attacking = true;
    }

    // Call this function whenever you want to do damage to all characters in range.
    public void StopAttack()
    {
        attacked.Clear();
        attacking = false;
    }

    public void DoAttack()
    {
        foreach (var e in inRange)
        {
            if (!attacked.Contains(e))
            {
                if (DEBUG_ATTACK)
                    Debug.Log("HIT!\n NAME: " + e.gameObject.name + " |||| TAG: " + e.gameObject.tag);

                e.Damage(damage);
                if (e.HP > 0)
                    attacked.Add(e);
                else
                    dead.Add(e);
            }
        }

        for (int i = 0; i < dead.Count; i++)
        {
            if (dead[i].HP <= 0)
            {
                inRange.Remove(dead[i]);
                dead[i].Die(_player);
                dead[i] = null;
            }
        }

        dead.Clear();
    }

    private void Update()
    {
        if(attacking)
            DoAttack();
    }
}

public class MeleeAttackHandler : AttackHandler
{

}
