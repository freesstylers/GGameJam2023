using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPiojos : MonoBehaviour
{
    public GameObject piojoPrefab;
    public GameObject piojoThrowPrefab;
    private Queue<GameObject> piojosPool_;
    private void Start()
    {
        piojosPool_ = new Queue<GameObject>();
    }

    public void SpawnPiojo()
    {
        GameObject newpiojo = Instantiate(piojoPrefab);
        newpiojo.transform.position = transform.position;
        newpiojo.GetComponent<WanderAround>().SetObjectToWander(gameObject);
        piojosPool_.Enqueue(newpiojo);
    }

    //Devuelve true si hay piojos que lanzar, y pierde uno
    public GameObject ThrowPiojo()
    {
        if (piojosPool_.Count > 0)
        {
            GameObject piojo = piojosPool_.Dequeue();
            Destroy(piojo);
            return piojoThrowPrefab;
        }
        else return null;
    }
}
