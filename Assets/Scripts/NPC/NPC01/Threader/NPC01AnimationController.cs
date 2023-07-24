using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class NPC01AnimationController : MonoBehaviour
{
    private NPC01ModelView modelView;

    private SpriteRenderer sprend;

    private Animator anim;

    public Dictionary<NPC01States, string> animationDictionary;

    private void Start()
    {
        animationDictionary = new Dictionary<NPC01States, string>();
        animationDictionary.Add(NPC01States.Idle, "idle");
        animationDictionary.Add(NPC01States.Throw, "range_attack");
        animationDictionary.Add(NPC01States.ReelinLeft, "reelin_left");
        animationDictionary.Add(NPC01States.ReelinRight, "reelin_right");
        animationDictionary.Add(NPC01States.MeleeAttackLeft, "melee_left");
        animationDictionary.Add(NPC01States.MeleeAttackRight, "melee_right");
        
        sprend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        modelView = GetComponentInParent<NPC01ModelView>();

        modelView.ChangeStateEvent += ChangeState;

        modelView.FacingRight += FlipSpriteX;
    }

    public void ChangeState(NPC01States key)
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