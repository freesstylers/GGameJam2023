using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaManager : MonoBehaviour
{
    public float completionTime_;

    private float timer_;
    public void SetCompletionTime(float newtime)
    {
        completionTime_ = newtime;
    }

    private void Start()
    {
        timer_ = 0.0f;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        timer_ += deltaTime;
    }
}
