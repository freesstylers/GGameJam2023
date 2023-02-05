using Rewired.UI;
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


                Vector2 _lookDir = ((Vector3)chargeObjective - transform.position).normalized;

                Animator animator = GetComponent<Animator>();

                if (_lookDir.y > 0)
                {
                    animator.SetInteger("Dir", (int)Look.UP);
                }
                else if (_lookDir.y < 0)
                {
                    animator.SetInteger("Dir", (int)Look.DOWN);
                }
                else if (_lookDir.x < 0)
                {
                    animator.SetInteger("Dir", (int)Look.LEFT);
                }
                else if (_lookDir.x > 0)
                {
                    animator.SetInteger("Dir", (int)Look.RIGHT);
                }

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

                Animator animator = GetComponent<Animator>();

                animator.SetInteger("Dir", -1);
            }
        }
    }
}
