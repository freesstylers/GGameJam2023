using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs_;

    public float density_;
    public int minEnemies_;
    public int maxEnemies_;

    private void Start()
    {
        RectTransform tr_ = GetComponent<RectTransform>();
        GenerarPelos(tr_);
    }

    private void GenerarPelos(RectTransform tr_)
    {

        float width = tr_.rect.width;
        float height = tr_.rect.height;

        float iniPosX = tr_.position.x - width / 2;
        float iniPosY = tr_.position.y - height / 2;

        int cantPelosFinal = Mathf.RoundToInt((minEnemies_ - maxEnemies_) * (density_ + Random.Range(-density_ / 10, density_ / 10))) + minEnemies_;

        List<Transform> pelosPositionsList = new List<Transform>();

        for (int pelo = 0; pelo < cantPelosFinal; pelo++)
        {
            GameObject thisEnemy = Instantiate(enemyPrefabs_[Random.Range(0, enemyPrefabs_.Length)]);
            Vector2 thisEnemyPos = new Vector2();

            bool safeZoned = false;
            while (!safeZoned)
            {
                safeZoned = true;

                thisEnemyPos.x = Random.Range(iniPosX + 10, iniPosX + width - 10);
                thisEnemyPos.y = Random.Range(iniPosY, iniPosY + height);

                foreach (Transform pos in pelosPositionsList)
                {
                    if (Vector2.Distance(thisEnemyPos, pos.position) < 2) safeZoned = false;
                }
            }

            thisEnemy.transform.position = thisEnemyPos;
            pelosPositionsList.Add(thisEnemy.transform);

            thisEnemy.transform.parent = null;
        }
    }

}
