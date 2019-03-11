using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTile
{
    public LevelModule[] possibleModules;   // Make this into a list or check number of modules left in another way

    public Vector2Int position = Vector2Int.zero;

    public LevelTile tileUp;
    public LevelTile tileDown;
    public LevelTile tileLeft;
    public LevelTile tileRight;

    public LevelTile(Vector2Int position, LevelModule[] possibleModules)
    {
        this.position = position;
        this.possibleModules = possibleModules;
        GameObject spawnedTile = GameObject.Instantiate(possibleModules[Random.Range(0, possibleModules.Length - 1)].go, (Vector2)position, Quaternion.identity);
        spawnedTile.name = position.ToString();
    }

    public void NoName()
    {
        int modulesLeft = ModulesLeft();
        if(modulesLeft == 1)
        {
            GameObject spawnedTile = GameObject.Instantiate(possibleModules[0].go, (Vector2)position, Quaternion.identity);
            spawnedTile.name = position.ToString();

            // Tell the others that a module has been picked
        }
        else if(modulesLeft <= 0)
            Debug.LogWarning("Out of possible modules. Algorithm failed");
    }

    int ModulesLeft()
    {
        int length = 0;
        for (int i = 0; i < possibleModules.Length; i++)
        {
            if(possibleModules[i])
                length++;
        }
        return length;
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

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, (Vector2)size);
    }
}
