using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerModelView modelView;

    private SpriteRenderer sprend;

    private Animator anim;

    public Dictionary<PlayerStates, string> animationDictionary;

    private void Start()
    {
        animationDictionary = new Dictionary<PlayerStates, string>();
        animationDictionary.Add(PlayerStates.Idle, "idle");
        animationDictionary.Add(PlayerStates.Crouch, "crouch");
        animationDictionary.Add(PlayerStates.Slide, "slide");
        animationDictionary.Add(PlayerStates.Run, "run");
        animationDictionary.Add(PlayerStates.StandMeleeAttack01, "standMeleeAttack01");
        animationDictionary.Add(PlayerStates.StandRangeAttack01, "standRangeAttack01");
        animationDictionary.Add(PlayerStates.Jump, "jump");
        animationDictionary.Add(PlayerStates.Fall, "jump");
        animationDictionary.Add(PlayerStates.TakeDamage, "takeDamage");
        
        sprend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        modelView = GetComponentInParent<PlayerModelView>();

        modelView.ChangeStateEvent += ChangeState;

        modelView.FacingRight += FlipSpriteX;
    }

    public void ChangeState(PlayerStates key)
    {
        string stateString;
        if (animationDictionary.TryGetValue(key, out stateString))
        {
            anim.Play(stateString);
        }
    }

    public void FlipSpriteX(bool facingRight)
    {
        if (facingRight)
            sprend.flipX = true;

        else
        {
            sprend.flipX = false;
        }
    }

    private void OnDisable()
    {
        modelView.ChangeStateEvent -= ChangeState;

        modelView.FacingRight -= FlipSpriteX;
    }
}