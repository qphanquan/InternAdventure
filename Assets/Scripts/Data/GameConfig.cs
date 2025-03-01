using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Dictionary<string, string> Obstancles;
}
public class GameConfig
{
    public LevelConfig[] LevelConfigs = new LevelConfig[3]
    {
        new LevelConfig(){Level = 1, Width = 3, Length = 10, Obstancles = new Dictionary<string, string>(){ 
                                                                            { "0_0", "Tree" },
                                                                            { "0_5", "Tree" },
                                                                            { "0_4", "Flower" },
                                                                            { "0_6", "Mushroom" },
                                                                            { "0_1", "Mushroom" },
                                                                            { "2_2", "Flower" },
                                                                            { "2_0", "Grass" },
                                                                            { "2_7", "Grass" },
                                                                            { "2_3", "Grass" },
                                                                            { "0_3", "Grass" },
                                                                            { "2_5", "Flower" }} },
        new LevelConfig(){Level = 2, Width = 3, Length = 15, Obstancles = new Dictionary<string, string>(){
                                                                            { "0_0", "Tree" },
                                                                            { "2_1", "Flower" },
                                                                            { "0_3", "Grass" },
                                                                            { "2_3", "Grass" },
                                                                            { "0_5", "Mushroom"},
                                                                            { "0_6", "Tree"},
                                                                            { "0_7", "Grass"},
                                                                            { "1_9", "Grass"},
                                                                            { "2_8", "Flower"},
                                                                            { "0_10", "Mushroom" },
                                                                            { "0_11", "Tree" },
                                                                            { "2_12", "Grass" }} },
        new LevelConfig(){Level = 3, Width = 3, Length = 18, Obstancles = new Dictionary<string, string>(){
                                                                            { "0_0", "Tree" },
                                                                            { "2_0", "Tree" },
                                                                            { "2_1", "Flower" },
                                                                            { "0_3", "Grass" },
                                                                            { "2_3", "Grass" },
                                                                            { "0_5", "Mushroom"},
                                                                            { "0_6", "Tree"},
                                                                            { "0_7", "Grass"},
                                                                            { "1_9", "Grass"},
                                                                            { "2_8", "Flower"},
                                                                            { "0_10", "Mushroom" },
                                                                            { "0_11", "Tree" },
                                                                            { "2_12", "Grass" },
                                                                            { "0_14", "Mushroom" },
                                                                            { "2_14", "Mushroom" },
                                                                            { "2_15", "Flower" },
                                                                            { "0_16", "Grass" },} },
    };
}
