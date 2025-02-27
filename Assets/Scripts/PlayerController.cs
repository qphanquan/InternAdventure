using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public Animator anim;

    private CharacterController _characters;
    private float _speedMove = 5f;

    void Start()
    {
        this._characters = this.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        float joyHorizontal = this.joystick.Horizontal;
        float joyVertical = this.joystick.Vertical;

        Vector3 joyHorizontalMove = joyHorizontal * this.transform.forward;
        Vector3 joyVerticalMove = -joyVertical * this.transform.right;

        Vector3 moveDirection = joyHorizontalMove + joyVerticalMove;
        this._characters.Move(moveDirection.normalized * this._speedMove * Time.deltaTime);
        if(joyHorizontal == 0 && joyVertical == 0)
        {
            this.anim.SetBool("isMoving", false);
        }
        else
        {
            this.anim.SetBool("isMoving", true);
        }
    }
}
