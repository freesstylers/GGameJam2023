using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyController : EnemyController
{
    Transform target;

    public float detectDistance;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(target.transform.position, transform.position) < detectDistance)
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
    }
}
