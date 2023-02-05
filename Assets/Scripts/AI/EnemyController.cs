using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float HP;

    public void Damage(float dmg)
    {
        HP -= dmg;

        if(HP > 0)
        {
            StartCoroutine(HitAnim(0.1f));
        }
    }

    IEnumerator HitAnim(float t)
{
    Color c = new Color(1, 0, 0);

    Color realC = new Color(1, 1, 1);

    float timer = 0.0f;

    while (timer < t)
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(c, realC, timer / t);

        yield return new WaitForEndOfFrame();

        timer += Time.deltaTime;
    }

    GetComponent<SpriteRenderer>().color = realC;
}

public void Die(PlayerInput player)
    {
        Vector2 deathDir = (transform.position - player.gameObject.transform.position).normalized;
        GetComponent<EnemyDeath>().SendFlyingAndDie(deathDir);
    }
}
