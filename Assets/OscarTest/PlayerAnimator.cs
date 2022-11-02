using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerMovement Movement;
    
    private Animator _anim;

    private SpriteRenderer _spr;

    private bool _facingRight;

    private int _moveInt;

    private bool _jumping;
    
    private void OnEnable()
    {
        _anim = GetComponent<Animator>();

        _spr = GetComponent<SpriteRenderer>();
    }

    public void ChangeMoveInt(int x)
    {
        _moveInt = x;
    }

    private void Update()
    {
        switch (_moveInt)
        {
            case 0:
                _anim.SetInteger("moveInt", 0);
                break;
            
            case 1:
                _anim.SetInteger("moveInt", 1);
                break;
            
            case 2:
                _anim.SetInteger("moveInt", 2);
                break;
            
            case 3:
                _anim.SetInteger("moveInt", 3);
                break;
        }

        _facingRight = Movement.FacingRight();
        if (_facingRight)
        {
            _spr.flipX=true;
        }
        else
        {
            _spr.flipX=false;
        }
    }
}
