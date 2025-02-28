using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public MapGenerator MapGenerator;
    public PlayerController PlayerController;

    private Camera _camera;
    private Vector3 _currentPosCamera;

    private float _followSpeed = 5f;
    private bool _isMoving;

    public bool IsMoving
    {
        get { return _isMoving; }
        set { _isMoving = value; }
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        this._camera = Camera.main;
        this._currentPosCamera = this._camera.transform.position;
        this.StartGame();
        this._camera.transform.position = this._currentPosCamera + this.PlayerController.transform.position;
    }

    private void Update()
    {
        if (!this.IsMoving)
            return;

        Vector3 newPosCamera = this._currentPosCamera + this.PlayerController.transform.position;
        this._camera.transform.position = Vector3.Slerp(this._camera.transform.position, newPosCamera, this._followSpeed * Time.deltaTime);
    }

    public void StartGame()
    {
        this.MapGenerator.InitMap(MainPlayerInfo.Instance.GetLevelConfig());
        GUIManager.Instance.ShowScreen<ScreenMain>();
        this.PlayerController.transform.position = this.MapGenerator.GetStartPosMap();
        this.PlayerController.ReloadPlayer(MainPlayerInfo.Instance.GetPlayer().Level);
    }
}

