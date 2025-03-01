using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager : MonoSingleton<ResourceManager>
{
    private GameConfig _gameConfig;
    private List<LevelConfig> _levelConfigs;
    private List<GameObject> _obstacles;
    private List<AudioClip> _audioClips;
    protected override void Awake()
    {
        base.Awake();
        if(this._gameConfig == null)
            this._gameConfig = new GameConfig();
        this.LoadGameConfigs();
        this.LoadObstacles();
        this.LoadAudioClips();
    }

    public void LoadGameConfigs()
    {
        if(this._levelConfigs == null)
            this._levelConfigs = new List<LevelConfig>();
        this._levelConfigs.Clear();
        foreach(var levelConfig in this._gameConfig.LevelConfigs)
        {
            if(levelConfig == null) continue;
            this._levelConfigs.Add(levelConfig);
        }
    }

    public void LoadObstacles()
    {
        if(this._obstacles == null)
            this._obstacles = new List<GameObject>();
        this._obstacles.Clear();
        GameObject[] obstaclesArr = Resources.LoadAll<GameObject>("Obstacle");
        this._obstacles.AddRange(obstaclesArr);
    }

    public void LoadAudioClips()
    {
        if (this._audioClips == null)
            this._audioClips = new List<AudioClip>();
        this._audioClips.Clear();
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>("AudioClips");
        this._audioClips.AddRange(audioClips);
    }

    public int MaxLevelConfig() => this._levelConfigs.Count;

    public LevelConfig GetLevel(int level)
    {
        LevelConfig levelConfig = this._levelConfigs.FirstOrDefault(a => a.Level == level);
        if (levelConfig == null)
            levelConfig = this._levelConfigs[0];
        return levelConfig;
    }

    public GameObject GetObstacle(string name)
    {
        GameObject obstacle = this._obstacles.FirstOrDefault(a => a.name.Equals(name));
        if(obstacle == null)
            obstacle = this._obstacles[0];
        return obstacle;
    }

    public AudioClip GetAudioClip(string name)
    {
        AudioClip audioClip = this._audioClips.FirstOrDefault(a => a.name.Equals(name));
        if(audioClip == null)
            audioClip = this._audioClips[0];
        return audioClip;
    }
}
