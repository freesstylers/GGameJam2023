using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeloSpawnerScript : MonoBehaviour
{
    public GameObject pelo_;

    // Densidad de pelos en pantalla, de 0.0 a 1.0
    public float density_;
    public int minPelos_;
    public int maxPelos_;

    public int peloDistance;

    void Start()
    {
        RectTransform tr_ = GetComponent<RectTransform>();

        float width = tr_.rect.width;
        float height = tr_.rect.height;

        float iniPosX = tr_.position.x-width/2;
        float iniPosY = tr_.position.y-height/2;

        int cantPelosFinal = Mathf.RoundToInt((maxPelos_ - minPelos_) * (density_ + Random.Range(-density_/10, density_/10))) + minPelos_;

        List<Transform> pelosPositionsList = new List<Transform>();

        for(int pelo = 0; pelo < cantPelosFinal; pelo++)
        {
            GameObject thisPelo = Instantiate(pelo_);
            Vector2 thisPeloPos = new Vector2();

            bool safeZoned = false;
            while (!safeZoned)
            {
                safeZoned = true;

                thisPeloPos.x = Random.Range(iniPosX, iniPosX + width);
                thisPeloPos.y = Random.Range(iniPosY, iniPosY + height);

                foreach (Transform pos in pelosPositionsList)
                {
                    if (Vector2.Distance(thisPeloPos, pos.position) < peloDistance) safeZoned = false;
                }
            }

            thisPelo.transform.position = thisPeloPos;
            pelosPositionsList.Add(thisPelo.transform);

            thisPelo.transform.parent = null;
        }

        pelosPositionsList.Sort((Transform t1, Transform t2) => { return t1.position.y.CompareTo(t2.position.y); });

        for(int i = 0; i < pelosPositionsList.Count; i++)
        {
            Transform aux = pelosPositionsList[i];
            aux.parent = tr_;
            aux.GetComponent<SpriteRenderer>().sortingOrder = pelosPositionsList.Count-i;
        }
    }


}
