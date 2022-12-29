using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{

    [SerializeField] private string _name;
    
    [SerializeField] private float _maxHP;

    [SerializeField] private float _moveSpeed;
    
    [SerializeField] private float _maxSpeed;

    [SerializeField] private float _attackTime;
    
    [SerializeField] private float _sightDistance;

    [SerializeField] private float _patrolTime;
    
    [SerializeField] private float _idleTime;

    public string MyName()
    {
        return (_name);
    }
    
    public float MyMaxHP()
    {
        return (_maxHP);
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
