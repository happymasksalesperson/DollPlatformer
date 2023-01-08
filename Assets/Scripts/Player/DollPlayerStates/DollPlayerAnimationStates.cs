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
    
    //
    private HealthModelView modelView;
    
    private void OnEnable()
    {
        modelView = GetComponentInParent<HealthModelView>();
        
        _anim = GetComponent<Animator>();

        _spr = GetComponent<SpriteRenderer>();
    }

    
   
}
