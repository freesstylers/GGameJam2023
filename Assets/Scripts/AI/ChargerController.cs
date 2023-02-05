using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerController : EnemyController
{
    Transform target;

    public float chargeDistance;
    public float chargeSpeed;
    public float chargeWindup;
    private float chargeTimer = 0.0f;
    private Vector2 chargeObjective;

    public float resetTime;
    private float resetTimer = 0.0f;

    public float detectDistance;

    private bool charging = false;
    private bool detected = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!detected) {
            if (Vector2.Distance(target.transform.position, transform.position) < detectDistance)
            {
                detected = true;
                chargeObjective = target.transform.position + (target.transform.position - transform.position).normalized * chargeDistance;
            }
        }
        else
        {
            Charge();
        }
    }

    void Charge()
    {
        chargeTimer += Time.deltaTime;
        if (chargeTimer > chargeWindup)
        {
            resetTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, chargeObjective, Time.deltaTime * chargeSpeed);
            if (transform.position == target.position || resetTimer > resetTime)
            {
                chargeTimer = 0.0f;
                resetTimer = 0.0f;
                detected = false;
            }
        }
    }
}
