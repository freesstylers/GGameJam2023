using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAround : MonoBehaviour
{
    [SerializeField]
    GameObject objectToWanderAround;

    Vector2 fin;
    Vector2 ini;
    public float intervaloX, intervaloY, intervaloXB, intervaloYB;
    public float minTime, maxTime;
    float time;
    float actualTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        ini = new Vector2(transform.position.x, transform.transform.position.y);
        fin = new Vector2(Random.Range(objectToWanderAround.transform.position.x - intervaloX, objectToWanderAround.transform.position.x + intervaloX), Random.Range(objectToWanderAround.transform.position.y - intervaloY, objectToWanderAround.transform.position.y + intervaloY));
        NewPosition();
        NewTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualTime < time)
        {
            actualTime += Time.deltaTime;
            transform.position = Vector2.Lerp(ini, fin, actualTime / time);
        }
        else
        {
            ini = actualTime >= time ? new Vector2(fin.x, fin.y) : new Vector2(transform.position.x, transform.position.y);
            actualTime = 0;
            NewPosition();
        }
    }

    void NewPosition()
    {
        float x = 0;
        float y = 0;

        NewTime();
        x = objectToWanderAround.transform.position.x;
        y = objectToWanderAround.transform.position.y;
        fin = new Vector2(Random.Range(x - intervaloX, x + intervaloX), Random.Range(y - intervaloY, y + intervaloY));
    }

    void NewTime()
    {
        time = Random.Range(minTime, maxTime);
    }
}
