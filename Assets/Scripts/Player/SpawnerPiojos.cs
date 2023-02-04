using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPiojos : MonoBehaviour
{
    public GameObject piojoPrefab;
    private Queue<GameObject> piojosPool_;

    private void Start()
    {
        piojosPool_ = new Queue<GameObject>();
    }

    public void SpawnPiojo()
    {
        GameObject newpiojo = Instantiate(piojoPrefab, transform);
        newpiojo.GetComponent<WanderAround>().SetObjectToWander(gameObject);
        piojosPool_.Enqueue(newpiojo);
    }
}
