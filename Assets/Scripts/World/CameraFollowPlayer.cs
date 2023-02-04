using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0, 0, -1);

    public float upperLimit;
    public float lowerLimit;

    void FixedUpdate()
    {
        Vector3 camerapos = new Vector3();
        camerapos.x = target.transform.position.x + offset.x;
        if(target.transform.position.y + offset.y < upperLimit && target.transform.position.y + offset.y > lowerLimit)
            camerapos.y = target.transform.position.y + offset.y;
        else
        {
            camerapos.y = transform.position.y;
        }
        camerapos.z = target.transform.position.z + offset.z;

        transform.position = camerapos;
    }

    public void SetStartingPosition(float y)
    {
        transform.position = new Vector3(transform.position.x, y, 0);
    }

    public void SetLimits(float upper, float lower)
    {
        upperLimit = upper;
        lowerLimit = lower;
    }
}
