using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenMain : ScreenBase
{
    public static ScreenMain Instance;
    public FixedJoystick joystick;
    public TMP_Text LevelTxt;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;

    }
    public override void OnInit(params object[] args)
    {
        base.OnInit(args);
        PlayerData player = (PlayerData)args[0];
        this.LevelTxt.text = string.Format("{0} {1}", "Level", player.Level);
    }

    public void OnJumpBtnClick()
    {
        GameController.Instance.PlayerController.OnJumpBtnClick();
    }

    public void OnHomeBtnClick()
    {
        GameController.Instance.MapGenerator.ClearMap();
        GUIManager.Instance.ShowScreen<ScreenLobby>(GameController.Instance.PlayerController);
        this.Hide();
    }
}
