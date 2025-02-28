using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    private GameConfig _gameConfig;
    private List<LevelConfig> _levelConfigs;
    protected override void Awake()
    {
        base.Awake();
        if(this._gameConfig == null)
            this._gameConfig = new GameConfig();
        this.LoadGameConfigs();
    }

    public void LoadGameConfigs()
    {
        if(this._levelConfigs == null)
            this._levelConfigs = new List<LevelConfig>();
        foreach(var levelConfig in this._gameConfig.LevelConfigs)
        {
            if(levelConfig == null) continue;
            this._levelConfigs.Add(levelConfig);
        }
    }

    public LevelConfig GetLevel(int level)
    {
        Debug.Log(level);
        LevelConfig levelConfig = this._levelConfigs.FirstOrDefault(a => a.Level == level);
        if (levelConfig == null)
            levelConfig = this._levelConfigs[0];
        return levelConfig;
    }
}
