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

    public void Excavate(SpawnerPiojos spawner)
    {
        gameObject.tag = "Untagged";

        GetComponent<Animator>().Play("SacarPelo");

        StartCoroutine(StopDig(spawner));
    }

    IEnumerator StopDig(SpawnerPiojos spawner)
    {
        yield return new WaitForEndOfFrame();

        string animName = GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;

        while (GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == animName)
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.AddHair(1, piojosCount_);
        for (int i = 0; i < piojosCount_; i++)
        {
            spawner.SpawnPiojo();
        }

        Debug.Log("A�adidos un pelo y " + piojosCount_ + " piojos");

        Destroy(gameObject);
    }
}
