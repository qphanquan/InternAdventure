using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerInfo : MonoSingleton<MainPlayerInfo>
{
    private PlayerData _data;
    private LevelConfig _levelConfig;
    protected override void Awake()
    {
        base.Awake();
        this.InitPlayer();
    }
    public void InitPlayer()
    {
        if(this._data == null)
            this._data = new PlayerData() { Level = 2};

        this._levelConfig = ResourceManager.Instance.GetLevel(this._data.Level);
    }

    public PlayerData GetPlayer() => this._data;
    public LevelConfig GetLevelConfig() => this._levelConfig;
}
