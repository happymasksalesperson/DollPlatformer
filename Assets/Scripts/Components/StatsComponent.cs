using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class StatsComponent : MonoBehaviour, ITakeDamage
{
    public Type type;

    public string myName;

    [Header("HEALTH POINTS AND VULNERABLE STATUS")]
    public int maxHP;
    public int HP; 
    
    public bool vulnerable;

    public bool isAlive = true;

    public bool armoured = false;
    
    [Header("MOVESPEED")]

    public float moveSpeed;

    public float maxSpeed;
    
    [Header("ATTACK 01 POWER, RADIUS & TIME")]

    public int attack01Power;

    public float attack01Radius;
    
    public float attack01Time;

    [Header("BODY HURTBOX ATTACK POWER")]
    public int bodyHitboxPower;
    
    // // // // // //
    //
    // sightDistance determines how far Character can see
    // minDist is used for triggering Attacks when in range
    [Header("SIGHT -- HOW FAR CHARACTER CAN SEE AND THE MINIMUM DISTANCE FOR ITS TARGET PROVOKE ATTACK")]
    public float sightDistance;

    public float minDist;
    
    [Header("HOW LONG SPENT PATROLLING AND IDLE")]
    public float patrolTime;

    public float idleTime;

    public HealthModelView modelView;

    public bool conjoined;

    [Header("TRACKS CURRENT FACING DIRECTION")]
    public bool facingDirection;

    private StateManager stateManager;

    private void OnEnable()
    {
        GameObject gameObj = gameObject;

        stateManager = GetComponent<StateManager>();

        modelView = GetComponentInChildren<HealthModelView>();

        HP = maxHP;

        LevelManager.levelManager.SFX.AddToList(gameObj, type);
        
        //LevelManager.levelManager.SFX.AddToDictionary(type, gameObj);
    }

    //changes HP
    public void ChangeHP(int amount)
    {
        if (vulnerable)
        {
            HP += amount;

            if (HP >= maxHP)
                HP = maxHP;

            modelView.OnChangeHealth(amount);

            if (HP <= 0)
            {
                isAlive = false;
                HP = 0;
                stateManager.ChangeStateString("death");
            }
            else
            stateManager.ChangeStateString("takeDamage");
        }

        if (amount > 0)
        {
            //heal
        }
    }

    //changes facing direction
    //currently used in DeathState to determine which way enemy is bumped back on death
    public void ChangeFacingDirection(bool dir)
    {
        //left falst right true
        facingDirection = dir;
    }
}