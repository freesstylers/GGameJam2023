using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public Vector2 cuadrado_;
    public int hNiveles_;
    public int vNiveles_;
    public int maxDistance_;
    public float minDistance_;
    [Range(1, 5)] public int maxNodos_;

    public GameObject prefab_; 
    //public GameObject ini_;
    public List<GameObject> eventos_;
    //public GameObject fin_;

    private List<GameObject> gm = new List<GameObject>();

    public void Start()
    {
        float ini = 0f, fin = 5f;
        float m = 5f;

        //for (int i = 1; i <= 5; i++)
        //{
            CreateLevel(ini, fin);
            //Crear nodos, devolver esos nodos y añadir los nuevos. Luego quitar los que no cumplan lo de la distancia
            //ini += m; fin += m;
        //}
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            gm.ForEach(x => Destroy(x));
            gm.Clear();
            gm = CreateLevel(0f, 5f);
        }
    }

    public void CreateMap()
    {

    }

    private List<GameObject> CreateLevel(float minY, float maxY)
    {
        int r = Random.Range(1, maxNodos_);
        float randomX = 0, lastRY = 0;
        List<GameObject> g = new List<GameObject>();
        //Creacion de nodos
        for (int i = 0; i < r; i++)
        {
            float randomY = Random.Range(minY, maxY);
            randomX = GetRandomWithMinDist(-cuadrado_.x, cuadrado_.x, randomY, randomX, lastRY);
            lastRY = randomY;

            g.Add(Instantiate(prefab_, new Vector2(randomX, randomY), Quaternion.identity));

        }

        return g;
    }

    private float GetRandomWithMinDist(float min, float max, float randomY, float lastRX, float lastRY)
    {
        float r = Random.Range(min, max);
        Vector2 other = new Vector2(lastRX, lastRY);
        Vector2 thisNode = new Vector2(r, randomY);

        while (Vector2.Distance(other, thisNode) < minDistance_)
        {            
            r = Random.Range(min, max);
            thisNode = new Vector2(r, randomY);
        }

        return r;
    }
}
