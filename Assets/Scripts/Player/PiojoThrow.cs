using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiojoThrow : MonoBehaviour
{
    private Vector2 direction;

    public float bulletSpeed;
    public int damageToEnemies;

    public float deathTimer;
    private float time = 0.0f;
    private void Update()
    {
        float deltaTime = Time.deltaTime;
        transform.position = new Vector3(transform.position.x + direction.x * deltaTime * bulletSpeed, transform.position.y + direction.y * deltaTime * bulletSpeed, 0);
        time += deltaTime;
        if (time > deathTimer) Destroy(gameObject);
    }

    public void SetDirection(Vector2 newdir)
    {
        direction = newdir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().Damage(damageToEnemies);
            Destroy(gameObject);
        }
    }
}
