using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/Generator Template")]
public class LevelGeneratorTemplate : ScriptableObject
{
    public LevelModule[] tiles;

    public LevelModule GetRandom()
    {
        return tiles[Random.Range(0, tiles.Length - 1)];
    }
}
