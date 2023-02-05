using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Animations;

public class PeloSpawnerScript : MonoBehaviour
{
    Tilemap tileMap_;
    public Tile[] paredesTile_;

    public Sprite[] pelosSprites_;
    public RuntimeAnimatorController[] animationControllers_;

    public GameObject pelo_;

    // Densidad de pelos en pantalla, de 0.0 a 1.0
    public float density_;
    public int minPelos_;
    public int maxPelos_;

    public int peloDistance_;

    public int paredWidth_;
    public int paredHorizontalDistanceVariance_;
    public float paredVariance_;
    private int pelosCount;

    private RectTransform tr_;

    private int biome_;

    void Start()
    {

        tileMap_ = GetComponent<Tilemap>();
        tr_ = GetComponent<RectTransform>();
        GenerarPelos();
        GenerarParedes();
    }

    private void OnEnable()
    {
        GenerarPelos();
    }

    public void SetBiome(int biome)
    {
        biome_ = biome;
    }

    public void GenerarPelos()
    {

        float width = tr_.rect.width;
        float height = tr_.rect.height;

        float iniPosX = tr_.position.x - width / 2;
        float iniPosY = tr_.position.y - height / 2;

        int cantPelosFinal = Mathf.RoundToInt((maxPelos_ - minPelos_) * (density_ + Random.Range(-density_ / 10, density_ / 10))) + minPelos_;

        List<Transform> pelosPositionsList = new List<Transform>();

        for (int pelo = 0; pelo < cantPelosFinal; pelo++)
        {
            GameObject thisPelo = Instantiate(pelo_);
            Vector2 thisPeloPos = new Vector2();

            bool safeZoned = false;
            while (!safeZoned)
            {
                safeZoned = true;

                thisPeloPos.x = Random.Range(iniPosX+paredWidth_, iniPosX + width-paredWidth_);
                thisPeloPos.y = Random.Range(iniPosY, iniPosY + height);

                foreach (Transform pos in pelosPositionsList)
                {
                    if (Vector2.Distance(thisPeloPos, pos.position) < peloDistance_) safeZoned = false;
                }
            }

            thisPelo.transform.position = thisPeloPos;
            pelosPositionsList.Add(thisPelo.transform);

            int piojosCount = Random.Range(0, 4);
            thisPelo.GetComponent<Pelo>().SetPiojos(piojosCount);

            if(piojosCount>0)
            {
                thisPelo.GetComponent<Animator>().runtimeAnimatorController = animationControllers_[1];
                thisPelo.GetComponent<SpriteRenderer>().sprite = pelosSprites_[1];
            }
            int rand = Random.Range(0, 2);
            if(rand == 1)
            {
                thisPelo.GetComponent<SpriteRenderer>().flipX = true;
            }

            thisPelo.transform.parent = null;
        }

        pelosPositionsList.Sort((Transform t1, Transform t2) => { return t1.position.y.CompareTo(t2.position.y); });

        for (int i = 0; i < pelosPositionsList.Count; i++)
        {
            Transform aux = pelosPositionsList[i];
            aux.parent = tr_;
            aux.GetComponent<SpriteRenderer>().sortingOrder = pelosPositionsList.Count - i;
        }

        pelosCount = pelosPositionsList.Count;
    }

    private void GenerarParedes()
    {
        //Posiciones iniciales eje X
        float iniPosXIzq = tr_.position.x - tr_.rect.width / 2.0f + paredWidth_;
        float iniPosXDer = tr_.position.x + tr_.rect.width / 2.0f - paredWidth_;
        float iniPosY = tr_.position.y - tr_.rect.height / 2.0f -1.5f;

        //Posiciones minimas y maximas de varianza en pared izquierda
        float minPosXIzq = iniPosXIzq - paredVariance_;
        float maxPosXIzq = iniPosXIzq + paredVariance_;

        //Posiciones minimas y maximas de varianza en pared derecha
        float minPosXDer = iniPosXDer - paredVariance_;
        float maxPosXDer = iniPosXDer + paredVariance_;

        //Posicion de la ultima pared colocada
        float lastXIzq = iniPosXIzq;
        float lastXDer = iniPosXDer;
        for (int i = 0; i < tr_.rect.height; i++)
        {
            //Paredes de izquierdas
            Vector2 thisParedPosIzq = new Vector2();

            thisParedPosIzq.x = Mathf.Max(minPosXIzq, lastXIzq + Random.Range(-paredHorizontalDistanceVariance_, paredHorizontalDistanceVariance_+1));
            thisParedPosIzq.x = (int)Mathf.Min(thisParedPosIzq.x, maxPosXIzq);
            thisParedPosIzq.y = iniPosY + i;
            
            //Paredes fachas
            Vector2 thisParedPosDer = new Vector2();

            thisParedPosDer.x = Mathf.Min(maxPosXDer, lastXDer + Random.Range(-paredHorizontalDistanceVariance_, paredHorizontalDistanceVariance_+1));
            thisParedPosDer.x = (int)Mathf.Max(thisParedPosDer.x, minPosXDer);
            thisParedPosDer.y = iniPosY + i;

            int paredIzqTile;
            int paredDerTile;
            if (lastXIzq < thisParedPosIzq.x) paredIzqTile = 3;
            else if (lastXIzq > thisParedPosIzq.x) paredIzqTile = 1;
            else paredIzqTile = 2;

            if (lastXDer < thisParedPosDer.x) paredDerTile = 6;
            else if (lastXDer > thisParedPosDer.x) paredDerTile = 4;
            else paredDerTile = 5;

            tileMap_.SetTile(new Vector3Int((int)thisParedPosIzq.x, (int)(iniPosY + i-1), 0), paredesTile_[paredIzqTile]);
            tileMap_.SetTile(new Vector3Int((int)thisParedPosDer.x, (int)(iniPosY + i-1), 0), paredesTile_[paredDerTile]);

            for(int ini = (int)(tr_.position.x - tr_.rect.width); ini < thisParedPosIzq.x+1; ini++)
            {
                tileMap_.SetTile(new Vector3Int(ini, (int)(iniPosY + i), 0), paredesTile_[0]);
            }

            for (int ini = (int)thisParedPosDer.x-1; ini < tr_.position.x + tr_.rect.width; ini++)
            {
                tileMap_.SetTile(new Vector3Int(ini+1, (int)(iniPosY + i), 0), paredesTile_[0]);
            }

            if (lastXIzq > thisParedPosIzq.x)
            {
                tileMap_.SetTile(new Vector3Int((int)thisParedPosIzq.x+1, (int)(iniPosY + i-1), 0), paredesTile_[1]);
                tileMap_.SetTile(new Vector3Int((int)thisParedPosIzq.x, (int)(iniPosY + i-1), 0), paredesTile_[0]);

            }
            if (lastXDer < thisParedPosDer.x)
            {
                tileMap_.SetTile(new Vector3Int((int)thisParedPosDer.x-1, (int)(iniPosY + i-1), 0), paredesTile_[6]);
                tileMap_.SetTile(new Vector3Int((int)thisParedPosDer.x, (int)(iniPosY + i-1), 0), paredesTile_[0]);
            }

            lastXIzq = thisParedPosIzq.x;
            lastXDer = thisParedPosDer.x;
        }

        tileMap_.GetComponent<TilemapRenderer>().sortingOrder = pelosCount + 1;
    }

}
