using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public MapGenerator MapGenerator;
    public PlayerController PlayerController;
    public ParticleSystem ConfettiVfx;

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
        GUIManager.Instance.ShowScreen<ScreenLobby>(this.PlayerController);
        SoundEfxManager.Instance.PlayMusicBackground();
        //this.StartGame();
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
        this.PlayerController.gameObject.SetActive(false);
        MainPlayerInfo.Instance.InitPlayer();
        this.MapGenerator.InitMap(MainPlayerInfo.Instance.GetLevelConfig());
        GUIManager.Instance.ShowScreen<ScreenMain>(MainPlayerInfo.Instance.GetPlayer());
        Vector3 starPosPlayer = this.MapGenerator.GetStartPosPlayer();
        Vector3 finalPos = this.MapGenerator.GetFinalPos();
        this.ConfettiVfx.gameObject.SetActive(false);
        this.ConfettiVfx.transform.position = finalPos;
        this.ConfettiVfx.gameObject.SetActive(true);
        this.StopParticle();
        this.PlayerController.transform.localPosition = new Vector3(starPosPlayer.x, 1f, starPosPlayer.z);
        this.PlayerController.ReloadPlayer();
        this.PlayerController.gameObject.SetActive(true);
        this._camera.transform.position = this._currentPosCamera + this.PlayerController.transform.position;
    }

    public void EndGame()
    {
        this.MapGenerator.ClearMap();
        GUIManager.Instance.HideScreen<ScreenMain>();
        GUIManager.Instance.ShowScreen<PopupEndGame>(MainPlayerInfo.Instance.GetPlayer());
        this._camera.transform.position = this._currentPosCamera;
        this.PlayerController.transform.position = Vector3.zero;
        this.PlayerController.transform.localScale = Vector3.one * 1.7f;
        this.PlayerController.anim.SetTrigger("Die1");
    }

    public void ResetCamera()
    {
        this._camera.transform.position = this._currentPosCamera;
    }

    public void PlayParticle() => this.ConfettiVfx.Play();
    public void StopParticle() => this.ConfettiVfx.Stop();
    public void CheckShowParticle(float z)
    {
        if(z >= this.MapGenerator.GetFinalPos().z - 4)
            this.PlayParticle();
        else
            this.StopParticle();
    }
}

