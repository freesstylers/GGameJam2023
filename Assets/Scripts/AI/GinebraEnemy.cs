using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GinebraEnemy : EnemyController
{
    public float HealthUp;

    public override void Die(PlayerInput player)
    {
        base.Die(player);

        player.TakeDamage(-HealthUp);
    }
}
