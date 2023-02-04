using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGenerator : MonoBehaviour
{
    public Tile[] tiles_;
    public Tilemap tileMap_;

    public RectTransform map_;

    private void Start()
    {
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
                tileMap_.SetTile(new Vector3Int(i, j, 0), tiles_[Random.Range(0, tiles_.Length)]);
            }
        }
    }

}
