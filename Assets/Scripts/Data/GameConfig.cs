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
    public List<string> Space;
    public Dictionary<string, string> Plants;
    public Dictionary<string, List<string>> Obstacle;

    public bool CheckValueObstacle(string value)
    {
        foreach (var item in Obstacle.Values)
            if (item.Equals(value))
                return true;
        return false;
    }
}
public class GameConfig
{
    public LevelConfig[] LevelConfigs = new LevelConfig[3]
    {
        new LevelConfig(){Level = 1, Width = 3, Length = 13, 
            Space = new List<string>(){"0_3", "1_3", "2_3", "0_4", "1_4", "2_4"}, 
            Obstacle = new Dictionary<string, List<string>>(){ 
                { "8_9", new List<string> { "0_8", "1_8", "2_8", "0_9", "1_9", "2_9" } } },
            Plants = new Dictionary<string, string>(){ 
                    { "0_0", "Tree" },
                    { "0_10", "Tree" },
                    { "0_6", "Flower" },
                    { "0_11", "Mushroom" },
                    { "0_1", "Mushroom" },
                    { "2_2", "Flower" },
                    { "2_0", "Grass" },
                    { "0_9", "Grass" },
                    { "2_5", "Grass" },
                    { "0_5", "Grass" },
                    { "2_6", "Flower" }} },
        new LevelConfig(){Level = 2, Width = 3, Length = 15,
            Space = null,
            Obstacle = new Dictionary<string, List<string>>(){
                {"3_4", new List<string>(){ "0_3", "1_3", "2_3", "0_4", "1_4", "2_4"} },
                { "8_9", new List<string> { "0_8", "1_8", "2_8", "0_9", "1_9", "2_9" } } },
            Plants = new Dictionary<string, string>(){
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
        new LevelConfig(){Level = 3, Width = 3, Length = 20,
            Space = new List<string>(){"0_3", "1_3", "2_3", "0_4", "1_4", "2_4"},
            Obstacle = new Dictionary<string, List<string>>(){
                { "8_9", new List<string> { "0_8", "1_8", "2_8", "0_9", "1_9", "2_9" } },
                { "14_15", new List<string> { "0_14", "1_14", "2_14", "0_15", "1_15", "2_15" } }},
            Plants = new Dictionary<string, string>(){
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
