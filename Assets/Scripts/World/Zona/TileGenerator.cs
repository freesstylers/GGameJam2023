using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    private Tile[][] tilesByBiome;
    public Tile[] tilesDefault_;
    public Tile[] tilesDermatitis_;
    public Tile[] tilesCaspa_;
    public Tile[] tilesDark_;
    public int biomaType;
    private Tilemap tileMap_;

    public RectTransform map_;

    private void Start()
    {
        biomaType = GameManager.instance.GetBiome();
        tileMap_ = GetComponent<Tilemap>();
        tilesByBiome = new Tile[4][];
        tilesByBiome[0] = tilesDefault_;
        tilesByBiome[1] = tilesDermatitis_;
        tilesByBiome[2] = tilesCaspa_;
        tilesByBiome[3] = tilesDark_;
        GenerateTiles();
    }

    public void GenerateTiles()
    {
        float width = map_.rect.width+10;
        float height = map_.rect.height+10;
        for (int i = (int)(map_.position.x - width/2); i < (int)(map_.position.x + width / 2); i++)
        {
            for (int j = (int)(map_.position.y - height/2); j < (map_.position.y + height / 2); j++)
            {
                tileMap_.SetTile(new Vector3Int(i, j, 0), tilesByBiome[biomaType][Random.Range(0, tilesByBiome[biomaType].Length)]);
            }
        }
    }

}
