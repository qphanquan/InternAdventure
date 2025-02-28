using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Level;
}
public class LevelConfig
{
    public int Level;
    public int Width;
    public int Length;
}
public class GameConfig
{
    public LevelConfig[] LevelConfigs = new LevelConfig[3]
    {
        new LevelConfig(){Level = 1, Width = 3, Length = 10},
        new LevelConfig(){Level = 2, Width = 3, Length = 15},
        new LevelConfig(){Level = 3, Width = 3, Length = 17},
    };
}
