using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public PlayerModelView modelView;

    public float SFXVolume;

    public static AudioClip
        Jumping,
        LandedSoft,
        LandedHard,
        Swing_01,
        Swing_02,
        Swing_03,
        TakeDamage_01,
        TakeDamage_02,
        TakeDamage_03,
        PlayerDeath;

    public AudioSource audioSrc;

    void Awake()
    {
        modelView.ChangeStateEvent += PlayStateSFX;

        modelView.DeclarePlayerMoment += PlayMomentSFX;

        audioSrc = GetComponent<AudioSource>();
        //example = Resources.Load<AudioClip>("example");

        Jumping = Resources.Load<AudioClip>("PlayerSFX/Jumping");

        LandedSoft = Resources.Load<AudioClip>("PlayerSFX/LandedSoft");
        LandedHard = Resources.Load<AudioClip>("PlayerSFX/LandedHard");

        Swing_01 = Resources.Load<AudioClip>("PlayerSFX/Swing_01");
        Swing_02 = Resources.Load<AudioClip>("PlayerSFX/Swing_02");
        Swing_03 = Resources.Load<AudioClip>("PlayerSFX/Swing_03");

        TakeDamage_01 = Resources.Load<AudioClip>("PlayerSFX/TakeDamage_01");
        TakeDamage_02 = Resources.Load<AudioClip>("PlayerSFX/TakeDamage_02");
        TakeDamage_03 = Resources.Load<AudioClip>("PlayerSFX/TakeDamage_03");
        PlayerDeath = Resources.Load<AudioClip>("PlayerSFX/PlayerDeath");
    }

    void OnDisable()
    {
        modelView.ChangeStateEvent -= PlayStateSFX;
        modelView.DeclarePlayerMoment -= PlayMomentSFX;
    }

    public void PlayStateSFX(PlayerStates inputState)
    {
        float randomValue = Random.value;

        switch (inputState)
        {
            case PlayerStates.Jump:
                audioSrc.PlayOneShot(Jumping);
                break;

            //TakeDamage_02 is my favorite, so it has a 66% chance of playing
            case PlayerStates.TakeDamage:


                if (randomValue <= 0.66f)
                {
                    audioSrc.PlayOneShot(TakeDamage_02);
                }
                else
                {
                    float randomClipValue = Random.value;

                    if (randomClipValue <= 0.5f)
                    {
                        audioSrc.PlayOneShot(TakeDamage_01);
                    }
                    else
                    {
                        audioSrc.PlayOneShot(TakeDamage_03);
                    }
                }

                break;

            //swoosh noises on stand attack
            //add to jump attacks?

            case (PlayerStates.StandMeleeAttack01):
                if (randomValue <= 0.33f)
                {
                    audioSrc.PlayOneShot(Swing_01);
                    break;
                }
                else if (randomValue <= 0.66f)
                {
                    audioSrc.PlayOneShot(Swing_02);
                    break;
                }
                else
                {
                    audioSrc.PlayOneShot(Swing_03);
                    break;
                }

            case (PlayerStates.Death):
                audioSrc.PlayOneShot(PlayerDeath);
                break;
        }
    }

    public void PlayMomentSFX(PlayerMoments inputMoment)
    {
        switch (inputMoment)
        {
            case (PlayerMoments.LandedSoft):
                audioSrc.PlayOneShot(LandedSoft);
                break;

            case (PlayerMoments.LandedHard):
                audioSrc.PlayOneShot(LandedHard);
                break;
        }
    }
}