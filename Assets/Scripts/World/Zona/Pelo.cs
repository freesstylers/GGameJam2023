using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pelo : MonoBehaviour
{
    private int piojosCount_;

    public void SetPiojos(int pipis)
    {
        piojosCount_ = pipis;
    }

    public int GetPiojos()
    {
        return piojosCount_;
    }
}
