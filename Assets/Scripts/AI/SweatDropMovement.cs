using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweatDropMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 aux = transform.position;
        aux.y += speed;
        transform.position = aux; 
    }
}
