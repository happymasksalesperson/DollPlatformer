using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class StatsComponent : MonoBehaviour, ITakeDamage
{
    public Type type;
    
    [SerializeField] private string _name;

    [SerializeField] private int maxHP;
    public int HP;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _attackTime;

    public int bodyHitboxPower;

    public int attack01Power;

    public float attack01Radius;

    public bool isAlive = true;

    public bool armoured = false;
    
    // // // // // //
    //
    // sightDistance determines how far Character can see
    // minDist is used for triggering Attacks when in range
    [SerializeField] private float _sightDistance;

    public float _minDist;

    [SerializeField] private float _patrolTime;

    [SerializeField] private float _idleTime;

    [SerializeField] private HealthModelView modelView;

    public bool vulnerable;

    public bool conjoined;

    public bool facingDirection;

    private StateManager stateManager;

    private void OnEnable()
    {
        GameObject gameObj = gameObject;

        stateManager = GetComponent<StateManager>();

        modelView = GetComponentInChildren<HealthModelView>();

        HP = maxHP;

        LevelManager.levelManager.SFX.AddToList(gameObj);
        
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

            stateManager.ChangeStateString("takeDamage");

            if (HP <= 0)
            {
                isAlive = false;
                HP = 0;
                stateManager.ChangeStateString("death");
            }
        }

        if (amount > 0)
        {
            //heal
        }
    }

    //kills NPC
    private void YouDied()
    {
    }

    public string MyName()
    {
        return (_name);
    }

    public float MyMaxHP()
    {
        return (maxHP);
    }

    public float MyMoveSpeed()
    {
        return (_moveSpeed);
    }

    public float MyMaxSpeed()
    {
        return (_maxSpeed);
    }

    public float MyAttackTime()
    {
        return (_attackTime);
    }

    public float MySightDistance()
    {
        return (_sightDistance);
    }

    public float MyPatrolTime()
    {
        return (_patrolTime);
    }

    public float MyIdleTime()
    {
        return (_idleTime);
    }

    //changes facing direction
    //currently used in DeathState to determine which way enemy is bumped back on death
    public void ChangeFacingDirection(bool dir)
    {
        //left falst right true
        facingDirection = dir;
    }
}