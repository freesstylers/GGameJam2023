using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public float flySpeed;
    public float flyForce;

    private Vector2 flyDir;

    public float deathTime;
    public float timer = 0.0f;

    private bool dead = false;

    public AudioClip deathSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<EnemyController>().GetComponent<AudioSource>();
    }
    public void SendFlyingAndDie(Vector2 dir)
    {
        dead = true;
        flyDir = dir;
        audioSource.clip = deathSound;
        audioSource.Play();
    }

    private void Update()
    {
        if(dead)
        {
            float deltaTime = Time.deltaTime;
            timer += deltaTime;
            if (timer < deathTime)
            {
                Vector2 newpos = new Vector2();

                newpos.x = transform.position.x + flyDir.x * deltaTime * flySpeed * flyForce;
                newpos.y = transform.position.y + flyDir.y * deltaTime * flySpeed * flyForce;

                flyForce -= deltaTime;

                transform.position = new Vector3(newpos.x, newpos.y, 0);
            } 
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
