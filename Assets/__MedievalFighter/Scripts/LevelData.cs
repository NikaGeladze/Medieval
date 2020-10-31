using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName =("LevelData"))]
public class LevelData : ScriptableObject
{

    public Color playerColor;
    public Color groundColor;
    public Color cubesColor;

    public int levelLength;
    public int playerID;
}
