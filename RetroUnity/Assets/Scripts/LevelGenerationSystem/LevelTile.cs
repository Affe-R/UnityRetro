using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTile
{
    public List<LevelModule> possibleModules = new List<LevelModule>();   // Make this into a list or check number of modules left in another way

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
        this.possibleModules.AddRange(possibleModules);
        // SpawnTile(0);
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

    // Module picked, origin = where the tiles is relative to this tile
    public void SomeonePickedAModule(LevelModule module, Direction origin)
    {
        for (int i = 0; i < possibleModules.Count; i++)
        {
            if(possibleModules[i] == null)
                continue;

            LevelModule[] moduleArray = null;

            switch (origin)
            {
                case Direction.Up:
                    moduleArray = possibleModules[i].possibleUp;
                    break;
                case Direction.Right:
                    moduleArray = possibleModules[i].possibleRight;
                    break;
                case Direction.Down:
                    moduleArray = possibleModules[i].possibleDown;
                    break;
                case Direction.Left:
                    moduleArray = possibleModules[i].possibleLeft;
                    break;
            }

            bool matchFound = false;
            for (int j = 0; j < moduleArray.Length; j++)
            {
                if(moduleArray[j] == module)
                {
                    matchFound = true;
                    break;
                }
            }
            if(!matchFound)
                possibleModules[i] = null;
        }
    }

    // public bool Contains(LevelModule module)
    // {
    //     for (int i = 0; i < possibleModules.Length; i++)
    //     {
    //         if(possibleModules[i] == module)
    //             return true;
    //     }

    //     return false;
    // }

    void SpawnTile(int index)
    {
        GameObject spawnedTile = GameObject.Instantiate(possibleModules[index].go, (Vector2)position, Quaternion.identity);
        spawnedTile.name = position.ToString();

        tileUp?.SomeonePickedAModule(possibleModules[index], Direction.Down);
        tileRight?.SomeonePickedAModule(possibleModules[index], Direction.Left);
        tileDown?.SomeonePickedAModule(possibleModules[index], Direction.Up);
        tileLeft?.SomeonePickedAModule(possibleModules[index], Direction.Right);

    }

    int ModulesLeft()
    {
        // Sort while searching
        // int length = 0;
        // for (int i = 0; i < possibleModules.Count; i++)
        // {
        //     if(possibleModules[i])
        //         length++;
        // }
        // return length;
        return possibleModules.Count;
    }

    int GetFirstModule()
    {
        // for (int i = 0; i < possibleModules.Length; i++)
        // {
        //     if(possibleModules[i])
        //         return i;
        // }
        // return -1;
        return 0;
    }

    public void RemovePossibleModule(LevelModule module)
    {
        // Remove Tile
        possibleModules.Remove(module);
        // Tell neighbors that the tile has been removed
        tileUp.RemovePossibleModule(module);

    }

    public void PickRandom()
    {
        int randomIndex = Random.Range(0, possibleModules.Count);
        for (int i = 0; i < possibleModules.Count; i++)
        {
            if(i != randomIndex)
                possibleModules[i] = null;
        }
    }

    public void Collapse()
    {
        if(possibleModules.Count > 1)
            PickRandom();
        
        // int firstModuleIndex = GetFirstModule();
        SpawnTile(0);

        tileUp?.Collapse();
        tileDown?.Collapse();
        tileRight?.Collapse();
        tileLeft?.Collapse();
    }
}