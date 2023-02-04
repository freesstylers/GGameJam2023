using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PeloSpawnerScript : MonoBehaviour
{
    private Tile[][] tilesByBiome;
    public Tile[] tilesDefault_;
    public Tile[] tilesDermatitis_;
    public Tile[] tilesCaspa_;
    public Tile[] tilesDark_;
    public int biomaType;
    Tilemap tileMap_;

    public GameObject pelo_;
    public GameObject pared_;

    // Densidad de pelos en pantalla, de 0.0 a 1.0
    public float density_;
    public int minPelos_;
    public int maxPelos_;

    public int peloDistance_;

    public int paredWidth_;
    public float paredHorizontalDistanceVariance_;
    public float paredVariance_;


    void Start()
    {
        tilesByBiome = new Tile[4][];
        tilesByBiome[0] = tilesDefault_;
        tilesByBiome[1] = tilesDermatitis_;
        tilesByBiome[2] = tilesCaspa_;
        tilesByBiome[3] = tilesDark_;
        tileMap_ = GetComponent<Tilemap>();
        RectTransform tr_ = GetComponent<RectTransform>();
        GenerarPelos(tr_);
        GenerarParedes(tr_);
    }

    private void GenerarPelos(RectTransform tr_)
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

            thisPelo.transform.parent = null;
        }

        pelosPositionsList.Sort((Transform t1, Transform t2) => { return t1.position.y.CompareTo(t2.position.y); });

        for (int i = 0; i < pelosPositionsList.Count; i++)
        {
            Transform aux = pelosPositionsList[i];
            aux.parent = tr_;
            aux.GetComponent<SpriteRenderer>().sortingOrder = pelosPositionsList.Count - i;
        }
    }

    private void GenerarParedes(RectTransform tr_)
    {
        //Posiciones iniciales eje X
        float iniPosXIzq = tr_.position.x - tr_.rect.width / 2 + paredWidth_;
        float iniPosXDer = tr_.position.x + tr_.rect.width / 2 - paredWidth_;
        float iniPosY = tr_.position.y - tr_.rect.height / 2;

        //Posiciones minimas y maximas de varianza en pared izquierda
        float minPosXIzq = iniPosXIzq - paredVariance_;
        float maxPosXIzq = iniPosXIzq + paredVariance_;

        //Posiciones minimas y maximas de varianza en pared derecha
        float minPosXDer = iniPosXDer - paredVariance_;
        float maxPosXDer = iniPosXDer + paredVariance_;

        //Lista de paredes para hacer el seteo de sprites correctos
        List<GameObject> paredesIzq = new List<GameObject>();
        List<GameObject> paredesDer = new List<GameObject>();

        //Posicion de la ultima pared colocada
        float lastXIzq = iniPosXIzq;
        float lastXDer = iniPosXDer;
        for (int i = 0; i < tr_.rect.height; i++)
        {
            //Paredes de izquierdas
            GameObject thisParedIzq = Instantiate(pared_);
            Vector2 thisParedPosIzq = new Vector2();

            thisParedPosIzq.x = Mathf.Max(minPosXIzq, lastXIzq + Random.Range(-paredHorizontalDistanceVariance_, paredHorizontalDistanceVariance_));
            thisParedPosIzq.x = (int)Mathf.Min(thisParedPosIzq.x, maxPosXIzq);
            thisParedPosIzq.y = iniPosY + i;

            thisParedIzq.transform.position = thisParedPosIzq;
            lastXIzq = thisParedPosIzq.x;
            thisParedIzq.transform.parent = tr_;
            paredesIzq.Add(thisParedIzq);
            
            //Paredes fachas
            GameObject thisParedDer = Instantiate(pared_);
            Vector2 thisParedPosDer = new Vector2();

            thisParedPosDer.x = Mathf.Min(maxPosXDer, lastXDer + Random.Range(-paredHorizontalDistanceVariance_, paredHorizontalDistanceVariance_));
            thisParedPosDer.x = (int)Mathf.Max(thisParedPosDer.x, minPosXDer);
            thisParedPosDer.y = iniPosY + i;

            if (lastXDer < thisParedPosDer.x) thisParedPosDer.x -= 1;
            thisParedDer.transform.position = thisParedPosDer;
            lastXDer = thisParedPosDer.x;
            thisParedDer.transform.parent = tr_;
            paredesDer.Add(thisParedDer);

            //Relleno de tiles entre paredes
            for (int x = (int)thisParedPosIzq.x-1; x < thisParedPosDer.x+1; x++)
            {
                tileMap_.SetTile(new Vector3Int(x, (int)(iniPosY + i), 0), tilesByBiome[biomaType][Random.Range(0, tilesByBiome[biomaType].Length)]);
            }
        }

        //Asignacion de sprites de pared
        paredesIzq[0].GetComponent<ParedScript>().setSprite(1);
        paredesDer[0].GetComponent<ParedScript>().setSprite(4);
        for(int i = 1; i < paredesIzq.Count; i++)
        {
            //Paredes izquierdas
            if(paredesIzq[i].transform.position.x < paredesIzq[i-1].transform.position.x)
            {
                paredesIzq[i - 1].GetComponent<ParedScript>().setSprite(0);
            } 
            else if (paredesIzq[i].transform.position.x > paredesIzq[i - 1].transform.position.x)
            {
                paredesIzq[i - 1].GetComponent<ParedScript>().setSprite(2);
                paredesIzq[i - 1].transform.position = new Vector2(paredesIzq[i - 1].transform.position.x + 1, paredesIzq[i - 1].transform.position.y);
            } 
            else
            {
                paredesIzq[i - 1].GetComponent<ParedScript>().setSprite(1);
            }

            //Paredes derechas
            if (paredesDer[i].transform.position.x < paredesDer[i - 1].transform.position.x)
            {
                paredesDer[i - 1].GetComponent<ParedScript>().setSprite(3);
            }
            else if (paredesIzq[i].transform.position.x > paredesIzq[i - 1].transform.position.x)
            {
                paredesDer[i - 1].GetComponent<ParedScript>().setSprite(5);
                paredesDer[i - 1].transform.position = new Vector2(paredesDer[i - 1].transform.position.x + 1, paredesDer[i - 1].transform.position.y);
            }
            else
            {
                paredesDer[i - 1].GetComponent<ParedScript>().setSprite(4);
            }
        }
    }

}
