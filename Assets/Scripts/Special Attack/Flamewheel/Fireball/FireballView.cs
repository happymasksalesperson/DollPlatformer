using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballView : MonoBehaviour
{
    public Animator anim;

    public FireballBrain brain;

    public SpriteRenderer spr;

    public void OnEnable()
    {
        brain.DeclareStateEvent += HandleStateChange;
        brain.DeclareFacingRight += FlipSprite;
    }

    public void OnDisable()
    {
        brain.DeclareStateEvent -= HandleStateChange;
        brain.DeclareFacingRight -= FlipSprite;
    }

    private void FlipSprite(bool input)
    {
        spr.flipX = input;
    }

    public void HandleStateChange(FireballBrain.FireballStateEnum newState)
    {
        string animState;

        switch (newState)
        {
            case FireballBrain.FireballStateEnum.Summon:
                animState = "FireballSummon";
                PlayFireBallAnimation(animState);
                break;
            case FireballBrain.FireballStateEnum.Projectile:
                animState = "FireballProjectile";
                PlayFireBallAnimation(animState);
                break;
            case FireballBrain.FireballStateEnum.Linger:
                animState = "FireballLinger";
                PlayFireBallAnimation(animState);
                break;
            case FireballBrain.FireballStateEnum.Death:
                animState = "FireballDeath";    
                PlayFireBallAnimation(animState);
                break;
        }
    }

    private void PlayFireBallAnimation(string clipName)
    {
        anim.Play(clipName);
    }
}