using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLobby : ScreenBase
{
    public override void OnInit(params object[] args)
    {
        base.OnInit(args);

        PlayerController player = (PlayerController)args[0];
        player.gameObject.SetActive(false);
        player.transform.localPosition = new Vector3(0, -3f, 0);
        player.transform.localEulerAngles = new Vector3(0, 90f, 0);
        player.transform.localScale = Vector3.one * 2f;
        player.gameObject.SetActive(true);
        player.anim.gameObject.SetActive(true);
        GameController.Instance.ResetCamera();
    }

    public void OnPlayBtnClick()
    {
        GameController.Instance.StartGame();
        this.Hide();
    }
}
