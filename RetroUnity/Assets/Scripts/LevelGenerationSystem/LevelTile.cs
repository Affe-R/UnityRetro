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

    public enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left
    }

    public LevelTile(Vector2Int position, LevelModule[] possibleModules)
    {
        this.position = position;
        this.possibleModules = possibleModules;
        SpawnTile(Random.Range(0, possibleModules.Length));
    }

    public void NoName()
    {
        int modulesLeft = ModulesLeft();
        if(modulesLeft == 1)
        {
            SpawnTile(0);

            // Tell the others that a module has been picked
        }
        else if(modulesLeft <= 0)
            Debug.LogWarning("Out of possible modules. Algorithm failed");
    }

    // Loop through possible modules
    // Check if the module fits

    // Module picked, origin = where the tiles is relative to this tile
    public void SomeonePickedAModule(LevelModule module, Direction origin)
    {
        switch (origin)
        {
            case Direction.Up:
                for (int i = 0; i < possibleModules.Length; i++)
                {
                    // if(possibleModules[i].possibleUp)
                }
                break;
            case Direction.Right:
                break;
            case Direction.Down:
                break;
            case Direction.Left:
                break;
        }

        for (int i = 0; i < possibleModules.Length; i++)
        {
            if(Contains(possibleModules[i]))
                break;
        }
    }

    public void FilterMatch(LevelModule module)
    {
        for (int i = 0; i < possibleModules.Length; i++)
        {
            // if(possibleModules[i].possib)
        }
    }

    public bool Contains(LevelModule module)
    {
        for (int i = 0; i < possibleModules.Length; i++)
        {
            if(possibleModules[i] == module)
                return true;
        }

        return false;
    }

    void SpawnTile(int index)
    {
        GameObject spawnedTile = GameObject.Instantiate(possibleModules[index].go, (Vector2)position + Vector2.one * .5f, Quaternion.identity);
        spawnedTile.name = position.ToString();
    }

    int ModulesLeft()
    {
        // Sort while searching
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