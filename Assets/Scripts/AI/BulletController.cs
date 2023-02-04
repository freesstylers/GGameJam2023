using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Vector2 direction;

    public float bulletSpeed;

    public float deathTimer;
    private float time = 0.0f;

    void Update()
    {
        float deltaTime = Time.deltaTime;
        time += deltaTime;
        if (time > deathTimer) Destroy(gameObject);

        transform.position = new Vector3(transform.position.x + direction.x * deltaTime * bulletSpeed, transform.position.y + direction.y * deltaTime * bulletSpeed, 0);
    }

    public void SetDirection(Vector2 newdir)
    {
        direction = newdir;
    }
}