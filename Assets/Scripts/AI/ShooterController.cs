using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : EnemyController
{
    Transform target;
    public GameObject bullet;

    public float shootCD;
    private float timer = 0.0f;

    public float detectDistance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(target.transform.position, transform.position) < detectDistance)
        {
            timer += Time.deltaTime;
            if(timer>shootCD)
            {
                GameObject newbullet = Instantiate(bullet);
                bullet.transform.position = transform.position;


                Vector2 normalizeddir = (target.position - transform.position).normalized;
                newbullet.GetComponent<BulletController>().SetDirection(normalizeddir);

                timer = 0.0f;
            }
        }
    }
}
