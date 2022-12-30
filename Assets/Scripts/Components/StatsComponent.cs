using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour, ITakeDamage
{

    [SerializeField] private string _name;
    
    [SerializeField] private int maxHP;
    private int HP;

    [SerializeField] private float _moveSpeed;
    
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _attackTime;
    
    [SerializeField] private float _sightDistance;

    [SerializeField] private float _patrolTime;
    
    [SerializeField] private float _idleTime;

    [SerializeField] private HealthModelView modelView;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<HealthModelView>();

        HP = maxHP;
    }

    //changes HP
    public void ChangeHP(int amount)
    {
        HP += amount;
        if (HP >= maxHP)
            HP = maxHP;

        modelView.OnChangeHealth(amount);
            
        if (HP <= 0)
        {
            HP = 0;
            modelView.OnYouDied();
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
}
