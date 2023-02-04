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

    void Start()
    {
        RectTransform tr_ = GetComponent<RectTransform>();

        float width = tr_.rect.width;
        float height = tr_.rect.height;

        float iniPosX = tr_.rect.position.x;
        float iniPosY = tr_.rect.position.y;

        int cantPelosFinal = Mathf.RoundToInt((maxPelos_ - minPelos_) * density_);

        for(int pelo = 0; pelo < cantPelosFinal; pelo++)
        {
            GameObject thisPelo = Instantiate(pelo_);
            Vector2 thisPeloPos = new Vector2();

            thisPeloPos.x = Random.Range(iniPosX, iniPosX+width);
            thisPeloPos.y = Random.Range(iniPosY, iniPosY+height);

            thisPelo.transform.position = thisPeloPos;
            thisPelo.transform.parent = tr_;
        }
    }


}
