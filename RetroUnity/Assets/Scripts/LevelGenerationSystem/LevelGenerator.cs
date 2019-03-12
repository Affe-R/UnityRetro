using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;
    public LevelTile[,] tiles;
    public LevelGeneratorTemplate template;

    public LevelTile rootTile;

    void Start()
    {
        tiles = CreateTiles(size);
        tiles[0, 0].Collapse();
    }

    LevelTile[,] CreateTiles(Vector2Int size)
    {
        tiles = new LevelTile[size.x, size.y];

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if(tiles[x, y] == null)
                {
                    LevelTile tile = new LevelTile(new Vector2Int(x, y), template.tiles);
                    tiles[x, y] = tile;

                    CreateTile(new Vector2Int(x, y + 1), ref tile.tileUp);
                    CreateTile(new Vector2Int(x, y - 1), ref tile.tileDown);
                    CreateTile(new Vector2Int(x + 1, y), ref tile.tileRight);
                    CreateTile(new Vector2Int(x - 1, y), ref tile.tileLeft);
                }
            }
        }

        return tiles;
    }

    void CreateTile(Vector2Int position, ref LevelTile tile)
    {
        bool withinBounds = position.x >= 0 && position.y >= 0 && position.x < size.x && position.y < size.y;
        if(withinBounds)
        {
            if(tiles[position.x, position.y] == null)
                tiles[position.x, position.y] = new LevelTile(new Vector2Int(position.x, position.y), template.tiles);
            tile = tiles[position.x, position.y];
        }
        else
            Debug.LogWarning("Index out of bounds");

    }

    void CollapseTiles()
    {
        int randomX = Random.Range(0, size.x - 1);
        int randomY = Random.Range(0, size.y - 1);      

        LevelTile tile = tiles[randomX, randomY];
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + (Vector3)((Vector2)size * .5f), (Vector2)size);
        Gizmos.color = Color.red;
        // Debug.Log(tiles.GetUpperBound(1));

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                LevelTile tile = tiles[x,y];
                Debug.Log(tile.position + " " + x + " " + y);

                if(tile.tileUp != null) Gizmos.DrawLine((Vector2)tile.position, (Vector2)tile.tileUp.position);
                if(tile.tileRight != null) Gizmos.DrawLine((Vector2)tile.position, (Vector2)tile.tileRight.position);
                if(tile.tileDown != null) Gizmos.DrawLine((Vector2)tile.position, (Vector2)tile.tileDown.position);
                if(tile.tileLeft != null) Gizmos.DrawLine((Vector2)tile.position, (Vector2)tile.tileLeft.position);
            }
        }
    }
}
