using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerInfo : MonoSingleton<MainPlayerInfo>
{
    private const string KeyLevelPlayer = "KeyLevelPlayer";

    private PlayerData _data;
    private LevelConfig _levelConfig;
    protected override void Awake()
    {
        base.Awake();
    }
    public void InitPlayer()
    {
        int keyLevel = PlayerPrefs.GetInt(KeyLevelPlayer);
        if (keyLevel == 0)
            keyLevel = 1;

        if(this._data == null)
            this._data = new PlayerData() { Level = keyLevel};

        PlayerPrefs.SetInt(KeyLevelPlayer, this._data.Level);
        this._levelConfig = ResourceManager.Instance.GetLevel(this._data.Level);
    }

    public void NextLevel()
    {
        this._data.Level += 1;
        if (this._data.Level > ResourceManager.Instance.MaxLevelConfig())
            this._data.Level = 1;
    }

    public PlayerData GetPlayer() => this._data;
    public LevelConfig GetLevelConfig() => this._levelConfig;
}
