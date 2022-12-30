using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DollPlayerAnimationStates : MonoBehaviour
{
    public DollPlayerMovement _playerMovement;
    
    private Animator _anim;

    private SpriteRenderer _spr;
    
    //anim stuff
    //refactor later w/states

    private bool _facingRight;

    private int _moveInt;

    private bool _jumping;

    private bool _isAttack;

    public int _attackInt;
    
    //
    private HealthModelView modelView;
    
    private void OnEnable()
    {
        modelView = GetComponentInParent<HealthModelView>();
        
        _anim = GetComponent<Animator>();

        _spr = GetComponent<SpriteRenderer>();
    }

    public void ChangeMoveInt(int x)
    {
        _moveInt = x;
    }

    public void ChangeAttackInt(int x)
    {
        _attackInt = x;
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

        _isAttack = _playerMovement.IsAttack();
        if(_isAttack)
        {
            _anim.SetBool("isAttack", true);
            _anim.SetInteger("attackInt", _attackInt);
        }
        else
        {
            _anim.SetBool("isAttack", false);
        }
        
        _facingRight = _playerMovement.FacingRight();
        if (_facingRight)
        {
            _spr.flipX=true;
        }
        else
            _spr.flipX=false;
    }
}
