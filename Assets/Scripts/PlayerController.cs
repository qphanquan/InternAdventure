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

    public void ReloadPlayer(int level)
    {
        if(this._characters == null)
            this._characters = this.GetComponent<CharacterController>();

        this._startPosPlayer = this.transform.position;
        this.anim.gameObject.SetActive(false);
        this._isJoystick = true;
        this._isDie = false;
        this.anim.gameObject.SetActive(true);
    }

    void Update()
    {
        if(!this._isJoystick || ScreenMain.Instance == null)
            return;

        Vector3 moveDirection = new Vector3(-ScreenMain.Instance.joystick.Direction.y, 0, ScreenMain.Instance.joystick.Direction.x);
        moveDirection.Normalize();
        this._characters.SimpleMove(moveDirection * this._moveSpeed);

        if (this.transform.position.y <= -8.5f)
        {
            //GameController.Instance.StartGame();
            Debug.LogError("ENDGAME");
            this._isJoystick = false;
        }

        if ((this._characters.collisionFlags & CollisionFlags.Sides) != 0)
        {
            Debug.LogError("WIN");
            this._isJoystick = false;
            //GameController.Instance.StartGame();
        }

        if (!this._isDie)
        {
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

            if (this._isJumping)
            {
                this._movingDirectionJump.y -= this._gravity * Time.deltaTime;
                this._characters.Move(this._movingDirectionJump * Time.deltaTime);
                if (this._characters.isGrounded)
                {
                    this._isJumping = false;
                }
            }

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
        this.anim.SetTrigger("Jump");
        this._movingDirectionJump.y = this._jumpHeight;
        this._isJumping = true;
    }
}
