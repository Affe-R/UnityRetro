using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level/Module")]
public class LevelModule : ScriptableObject
{
    public GameObject go;
    public Sprite sprite;

    public LevelModule[] possibleUp;
    public LevelModule[] possibleDown;
    public LevelModule[] possibleLeft;
    public LevelModule[] possibleRight;

}
