using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs_;

    public float density_;
    public int minEnemies_;
    public int maxEnemies_;
    RectTransform tr_;


    private void OnEnable()
    {
        tr_ = GetComponent<RectTransform>();
        GenerarEnemies();
    }

    public void GenerarEnemies()
    {

        float width = tr_.rect.width;
        float height = tr_.rect.height;

        float iniPosX = tr_.position.x - width / 2;
        float iniPosY = tr_.position.y - height / 2;

        int cantPelosFinal = Mathf.RoundToInt((minEnemies_ - maxEnemies_) * (density_ + Random.Range(-density_ / 10, density_ / 10))) + minEnemies_;

        List<Transform> pelosPositionsList = new List<Transform>();

        for (int pelo = 0; pelo < cantPelosFinal; pelo++)
        {
            int rand = Random.Range(0, 100);
            int enemyCount = 0;
            if(rand<40)
            {
                enemyCount = 2;
            } else if (rand >= 40 && rand < 70)
            {
                enemyCount = 0;
            }
            else if (rand >= 70 && rand < 90)
            {
                enemyCount = 1;
            }else
            {
                enemyCount = 3;
            }
            GameObject thisEnemy = Instantiate(enemyPrefabs_[enemyCount]);
            Vector2 thisEnemyPos = new Vector2();

            thisEnemyPos.x = Random.Range(iniPosX + 10, iniPosX + width - 10);
            thisEnemyPos.y = Random.Range(iniPosY, iniPosY + height/2);

            thisEnemy.transform.position = thisEnemyPos;
            pelosPositionsList.Add(thisEnemy.transform);

            thisEnemy.transform.parent = null;
        }
    }

}
