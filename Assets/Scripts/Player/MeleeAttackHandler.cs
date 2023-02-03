using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    [SerializeField]
    bool DEBUG_ATTACK = false;

    PlayerInput _player;
    public float damage;

    public void Initialize(PlayerInput p)
    {
        _player = p;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DEBUG_ATTACK)
            Debug.Log("HIT!\n NAME: " + collision.gameObject.name + " |||| TAG: " + collision.gameObject.tag);

        //daño? ??
    }
}

public class MeleeAttackHandler : AttackHandler
{

}
