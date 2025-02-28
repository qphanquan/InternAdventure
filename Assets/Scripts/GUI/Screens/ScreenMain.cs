using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMain : ScreenBase
{
    public static ScreenMain Instance;
    public FixedJoystick joystick;

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
    }

    public void OnJumpBtnClick()
    {
        GameController.Instance.PlayerController.OnJumpBtnClick();
    }
}
