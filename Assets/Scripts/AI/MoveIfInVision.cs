using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIfInVision : MonoBehaviour
{
    Collider2D visionCollider;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        visionCollider = GetComponent<Collider2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool seeingPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if (seeingPlayer)
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            seeingPlayer = true;
            Debug.Log("te veo");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            seeingPlayer = false;
            Debug.Log("ya no te veo");
        }
    }
}
