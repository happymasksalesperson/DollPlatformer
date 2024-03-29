using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPCSFX : MonoBehaviour
{
    private NPCModelView modelView;
    public List<GameObject> NPC02List = new List<GameObject>();

    public int[] soundTypes;
    public int music = 0;
    public int SFX = 1;

    public float musicVolume;
    public float SFXVolume;

    public static AudioClip
        
        //SONGS
        Chase,
        Cymbal_Boss,
        Father,
        The_Clocktower,
        Somber,
        
        //NPC 02
        NPC02_Charge01,
        NPC02_Charge02,
        NPC02_Charge03,
        NPC02_Death01,
        NPC02_Death02,
        NPC02_Death03,
        NPC02_HardHit,
        NPC02_HardRoll01,
        NPC02_HardRoll02,
        NPC02_MidRoll,
        NPC02_SoftRoll01,
        NPC02_SoftRoll02;

    static AudioSource audioSrc;

    public static string currentSong;
    
    void Awake()
    {
       soundTypes = new[] { music, SFX };

       audioSrc = GetComponent<AudioSource>();
        //example = Resources.Load<AudioClip>("example");

        Chase = Resources.Load<AudioClip>("Songs/Chase");
        Cymbal_Boss = Resources.Load<AudioClip>("Songs/Cymbal_Boss");
        Father = Resources.Load<AudioClip>("Songs/Father");
        The_Clocktower = Resources.Load<AudioClip>("Songs/The_Clocktower");
        Somber = Resources.Load<AudioClip>("Songs/Somber");
        
        //change this later (not every level has to load every NPC sound)
        //doesn't need NPC_02 in string
        NPC02_Charge01 = Resources.Load<AudioClip>("NPC02/NPC02_Charge01");
        NPC02_Charge02 = Resources.Load<AudioClip>("NPC02/NPC02_Charge02");
        NPC02_Charge03 = Resources.Load<AudioClip>("NPC02/NPC02_Charge03");
        NPC02_Death01 = Resources.Load<AudioClip>("NPC02/NPC02_Death01");
        NPC02_Death02 = Resources.Load<AudioClip>("NPC02/NPC02_Death02");
        NPC02_Death03 = Resources.Load<AudioClip>("NPC02/NPC02_Death03");
        NPC02_HardHit = Resources.Load<AudioClip>("NPC02/NPC02_HardHit");
        NPC02_HardRoll01 = Resources.Load<AudioClip>("NPC02/NPC02_HardRoll01");
        NPC02_HardRoll02 = Resources.Load<AudioClip>("NPC02/NPC02_HardRoll02");
        NPC02_MidRoll = Resources.Load<AudioClip>("NPC02/NPC02_MidRoll");
        NPC02_SoftRoll01 = Resources.Load<AudioClip>("NPC02/NPC02_SoftRoll01");
        NPC02_SoftRoll02 = Resources.Load<AudioClip>("NPC02/NPC02_SoftRoll02");
        
    }
    
    /*public Dictionary<Type, GameObject> myDictionary = new Dictionary<Type, GameObject>();

    public void AddToDictionary(Type type, GameObject gameObject)
    {
        //myDictionary.Add(type, gameObject);
        
      //  if(type==Type.NPC02)
            //AddToList(gameObject);
    }*/

    //refactor later
    public void AddToList(GameObject gameObject, Type type)
    {
        switch (type)
        {
            case Type.NPC02:
                NPC02List.Add(gameObject);
                break;
        }
        
        Subscribe();
    }

    public void Unsubscribe(GameObject gameObject)
    {
        NPC02List.Remove(gameObject);
        if (gameObject.GetComponentInChildren<NPCModelView>() != null)
        {
            modelView = gameObject.GetComponentInChildren<NPCModelView>();

            modelView.Attack01Windup -= Attack01Windup;
            modelView.Attack01 -= Attack01;
            modelView.Patrol -= Patrol;
            modelView.Death -= Death;
        }
    }

    private void Subscribe()
    {
        foreach (GameObject npcModelView in NPC02List)
        {
            if (npcModelView.GetComponentInChildren<NPCModelView>() != null)
            {
                modelView = npcModelView.GetComponentInChildren<NPCModelView>();

                modelView.Attack01Windup += Attack01Windup;
                modelView.Attack01 += Attack01;

                //modelView.Patrol += Patrol;

                modelView.TakeDamage += TakeDamage;

                modelView.Death += Death;
            }
        }
    }

    private void Attack01Windup()
    {
        float rand = Random.value;
        if (rand < 0.3f)
            PlaySound("NPC02_Charge01");

        if (rand > 0.33f && rand < 0.66f)
            PlaySound("NPC02_Charge02");

        else
            PlaySound("NPC02_Charge03");
    }

    private void Attack01()
    {
        PlaySound("NPC02_HardHit");
    }

    private void Patrol()
    {
        if (Random.value < 0.5f)
            PlaySound("NPC02_SoftRoll01");

        else
            PlaySound("NPC02_SoftRoll02");
    }

    private void TakeDamage()
    {
        float rand = Random.value;
        if (rand < 0.3f)
            PlaySound("NPC02_MidRoll");

        if (rand > 0.33f && rand < 0.66f)
            PlaySound("NPC02_HardRoll01");

        else
            PlaySound("NPC02_HardRoll02");
    }

    private void Death()
    {
        float rand = Random.value;
        if (rand < 0.3f)
            PlaySound("NPC02_Death01");

        if (rand > 0.33f && rand < 0.66f)
            PlaySound("NPC02_Death02");

        else
            PlaySound("NPC02_Death03");
    }

    public static void ChangeVolume(float amount)
    {
        audioSrc.volume = amount;
    }

    public static void PlaySound(string clipName)
    {
        currentSong = clipName;
        
        switch (clipName)
        {
            
            case "Chase":
                audioSrc.PlayOneShot(Chase);
                break;
            
            case "Cymbal_Boss":
                audioSrc.PlayOneShot(Cymbal_Boss);
                break;
            
            case "Father":
                audioSrc.PlayOneShot(Father);
                break;
            
            case "The_Clocktower":
                audioSrc.PlayOneShot(The_Clocktower);
                break;
            
            case "Somber":
                audioSrc.PlayOneShot(Somber);
                break;
            
            case "NPC02_Charge01":
                audioSrc.PlayOneShot(NPC02_Charge01);
                break;

            case "NPC02_Charge02":
                audioSrc.PlayOneShot(NPC02_Charge02);
                break;

            case "NPC02_Charge03":
                audioSrc.PlayOneShot(NPC02_Charge03);
                break;

            case "NPC02_Death01":
                audioSrc.PlayOneShot(NPC02_Death01);
                break;

            case "NPC02_Death02":
                audioSrc.PlayOneShot(NPC02_Death02);
                break;

            case "NPC02_Death03":
                audioSrc.PlayOneShot(NPC02_Death03);
                break;

            case "NPC02_HardHit":
                audioSrc.PlayOneShot(NPC02_HardHit);
                break;

            case "NPC02_HardRoll01":
                audioSrc.PlayOneShot(NPC02_HardRoll01);
                break;

            case "NPC02_HardRoll02":
                audioSrc.PlayOneShot(NPC02_HardRoll02);
                break;

            case "NPC02_MidRoll":
                audioSrc.PlayOneShot(NPC02_MidRoll);
                break;

            case "NPC02_SoftRoll01":
                audioSrc.PlayOneShot(NPC02_SoftRoll01);
                break;

            case "NPC02_SoftRoll02":
                audioSrc.PlayOneShot(NPC02_SoftRoll02);
                break;
        }
    }

    private void OnDisable()
    {
    }
}