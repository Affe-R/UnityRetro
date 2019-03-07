using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile
{
    public LevelModule[] possibleModules;

    public Vector2 position = Vector2.zero;

    public LevelTile tileUp;
    public LevelTile tileDown;
    public LevelTile tileLeft;
    public LevelTile tileRight;

    public LevelTile(Vector2 position)
    {
        this.position = position;
    }

    public void RemovePossibleModule(LevelModule module)
    {
        
    }

    public int GetPossibleTilesCount()
    {
        return possibleModules.Length;
    }
}

public class LevelGenerator : MonoBehaviour
{
    public Vector2Int size = Vector2Int.one;
    public LevelTile[,] tiles;
    public LevelGeneratorTemplate template;

    public LevelTile rootTile;

    void Start()
    {
        tiles = CreateTiles(size);
    }

    void CreateTiless(LevelTile tile)
    {
        if(tile.tileUp == null)
        {  
            tile.tileUp = new LevelTile(tile.position + new Vector2(0, 1));
            tile.tileUp.possibleModules = template.tiles;
        }
        if(tile.tileDown == null)
        {  
            tile.tileDown = new LevelTile(tile.position + new Vector2(0, -1));
            tile.tileDown.possibleModules = template.tiles;
        }
        if(tile.tileLeft == null)
        {  
            tile.tileLeft = new LevelTile(tile.position + new Vector2(-1, 0));
            tile.tileLeft.possibleModules = template.tiles;
        }
        if(tile.tileRight == null)
        {  
            tile.tileRight = new LevelTile(tile.position + new Vector2(1, 0));
            tile.tileRight.possibleModules = template.tiles;
        }

    }

    LevelTile[,] CreateTiles(Vector2Int size)
    {
        LevelTile[,] tiles = new LevelTile[size.x, size.y];

        for (int y = 0; y < tiles.GetUpperBound(0); y++)
        {
            for (int x = 0; x < tiles.GetUpperBound(1); x++)
            {
                LevelTile tile = new LevelTile();
                tile.possibleModules = template.tiles;
                tiles[x, y] = tile;
            }
        }

        return tiles;
    }

    void CollapseTiles()
    {
        int randomX = Random.Range(0, size.x - 1);
        int randomY = Random.Range(0, size.y - 1);      

        LevelTile tile = tiles[randomX, randomY];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, (Vector2)size);
    }

}
