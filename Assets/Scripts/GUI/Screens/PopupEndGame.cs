using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupEndGame : ScreenBase
{
    public TMP_Text LevelTxt;

    public override void OnInit(params object[] args)
    {
        base.OnInit(args);
        PlayerData playerData = (PlayerData)args[0];

        this.LevelTxt.text = string.Format("{0} {1}", "Level", playerData.Level);
    }

    public void OnReplayBtnClick()
    {
        //GameController.Instance.PlayerController.gameObject.SetActive(false);
        GameController.Instance.StartGame();
        this.Hide();
    }

    public void OnSkipLevelBtnClick()
    {
        MainPlayerInfo.Instance.NextLevel();
        GameController.Instance.StartGame();
        this.Hide();
    }
}
