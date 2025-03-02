using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    private CharacterController _characters;
    private Vector3 _movingDirectionJump = Vector3.zero;
    private Vector3 _startPosPlayer;

    private float _moveSpeed = 5f;
    private float _rotationSpeed = 5f;
    private float _jumpHeight = 10f;
    private float _gravity = 10f;

    private bool _isJumping;
    private bool _isJoystick;
    private bool _isDie;
    private bool _isDieObstacle;

    public void ReloadPlayer()
    {
        if(this._characters == null)
            this._characters = this.GetComponent<CharacterController>();

        this.transform.localScale = Vector3.one;
        this.transform.localEulerAngles = Vector3.zero;
        this._startPosPlayer = this.transform.position;
        this.anim.gameObject.SetActive(false);
        this.anim.gameObject.SetActive(true);
        this._isJoystick = true;
        this._isDie = false;
        this._isJumping = false;
    }

    void Update()
    {
        if(!this._isJoystick || ScreenMain.Instance == null)
            return;

        Vector3 moveDirection = new Vector3(-ScreenMain.Instance.joystick.Direction.y, 0, ScreenMain.Instance.joystick.Direction.x);
        moveDirection.Normalize();
        this._characters.SimpleMove(moveDirection * this._moveSpeed);

        if (this.transform.position.y <= -7f || (this._isDieObstacle && this.transform.position.y < -4f))
        {
            Debug.LogError("ENDGAME");
            SoundEfxManager.Instance.PlaySoundLose();
            GameController.Instance.EndGame();
            this._isJoystick = false;
        }

        GameController.Instance.CheckShowParticle(this.transform.position.z);

        if (!this._isDie)
        {
            this._isDieObstacle = GameController.Instance.MapGenerator.CheckObstacle(this.transform.position.z);

            if (moveDirection.magnitude == 0)
            {
                this.anim.SetBool("isMoving", false);
                GameController.Instance.IsMoving = false;
            }
            else
            {
                this.anim.SetBool("isMoving", true);
                GameController.Instance.IsMoving = true;
                var targetDirection = Vector3.RotateTowards(this.transform.forward, moveDirection, this._rotationSpeed * Time.deltaTime, 0.0f);
                this.transform.rotation = Quaternion.LookRotation(targetDirection);
            }

            if (this._isJumping && this.transform.position.y > 0)
            {
                this._movingDirectionJump.y -= this._gravity * Time.deltaTime;
                this._characters.Move(this._movingDirectionJump * Time.deltaTime);
                if (this._characters.isGrounded)
                {
                    this._isJumping = false;
                }
            }
            else
                this._isJumping = false;


            if (!this._characters.isGrounded && !this._isJumping)
            {
                if (this.transform.position.y <= 0f)
                {
                    this.anim.SetTrigger("Die");
                    this._isDie = true;
                }
            }
        }
    }

    public void OnJumpBtnClick()
    {
        if (this._isJumping || !this._characters.isGrounded)
            return;
        SoundEfxManager.Instance.PlaySoundJump();
        this.anim.SetTrigger("Jump");
        this._movingDirectionJump.y = this._jumpHeight;
        this._isJumping = true;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((this._characters.collisionFlags & CollisionFlags.Sides) != 0 && hit.gameObject.layer == LayerMask.NameToLayer("House"))
        {
            Debug.LogError("WIN");
            this._isJoystick = false;
            SoundEfxManager.Instance.PlaySoundWin();
            MainPlayerInfo.Instance.NextLevel();
            GameController.Instance.StartGame();
            //this.anim.gameObject.SetActive(false);
        }
    }
}
