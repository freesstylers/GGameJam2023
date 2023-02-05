using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    //Arrays de los tiles de cada "bioma"
    private Tile[][] tilesByBiome;
    public Tile[] tilesDefault_;
    public Tile[] tilesDermatitis_;
    public Tile[] tilesCaspa_;
    public Tile[] tilesDark_;
    public int biomaType;
    private Tilemap tileMap_;

    public RectTransform map_;

    public ChampuController champu_;
    public CameraFollowPlayer camera_;
    public PlayerInput player_;
    public FinishZone finish_;
    public PeloSpawnerScript peloSpawner_;
    public EnemySpawner enemySpawner_;

    private void OnEnable()
    {
        biomaType = GameManager.instance.GetBiome();
        tileMap_ = GetComponent<Tilemap>();
        tilesByBiome = new Tile[4][];
        tilesByBiome[0] = tilesDefault_;
        tilesByBiome[1] = tilesDermatitis_;
        tilesByBiome[2] = tilesCaspa_;
        tilesByBiome[3] = tilesDark_;

        champu_.SetMapSize(map_.rect.height);
        champu_.SetStartingPos(new Vector2(0, map_.position.y - map_.rect.height / 2 - 5));

        camera_.SetLimits(map_.position.y + map_.rect.height / 2 - map_.rect.height / 10, map_.position.y - map_.rect.height / 2+ map_.rect.height / 10);

        camera_.SetStartingPosition(map_.position.y - map_.rect.height / 2 + map_.rect.height / 10);
        player_.SetStartingPosition(map_.position.y - map_.rect.height / 2 + map_.rect.height / 10);

        finish_.SetPosition(map_.position.y + map_.rect.height / 2 - map_.rect.height / 10);

        peloSpawner_.SetBiome(biomaType);

        GenerateTiles();
        peloSpawner_.GenerarPelos();
    }
    
    

    //Llenamos el tilemap con los tiles de piel caminables
    public void GenerateTiles()
    {
        float width = map_.rect.width+10;
        float height = map_.rect.height+10;
        for (int i = (int)(map_.position.x - width/2); i < (int)(map_.position.x + width / 2); i++)
        {
            for (int j = (int)(map_.position.y - height/2); j < (map_.position.y + height / 2); j++)
            {
                int randomTile = Random.Range(0, tilesByBiome[biomaType].Length + 6);
                if (randomTile > 3) randomTile = 3;
                tileMap_.SetTile(new Vector3Int(i, j, 0), tilesByBiome[biomaType][randomTile]);
            }
        }
    }

}
