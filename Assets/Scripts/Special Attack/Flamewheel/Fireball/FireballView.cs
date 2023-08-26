using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballView : MonoBehaviour
{
    public Animator anim;

    public FireballBrain brain;

    public void OnEnable()
    {
        brain.declareStateEvent += HandleStateChange;
    }

    public void OnDisable()
    {
        brain.declareStateEvent -= HandleStateChange;
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