using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField]
    float xRange;
    [SerializeField]
    float yRange;
    [SerializeField]
    float timeToChange;

    float currentTime = 0.0f;
    Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime < timeToChange)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, currentTime);
        }
        else
        {
            currentTime = 0.0f;
            newPos = generateNewPos();
        }
    }

    Vector3 generateNewPos()
    {
        float randX = Random.Range(-xRange, xRange);
        float randY = Random.Range(-yRange, yRange);
        Vector3 ret = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);
        return ret;
    }
}
