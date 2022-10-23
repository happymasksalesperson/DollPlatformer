using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsComponent : MonoBehaviour
{

    [SerializeField] private string _name;
    
    [SerializeField] private float _maxHP;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _attackPower;
    
    [SerializeField] private float _sightDistance;

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
    
    public float MyAttackPower()
    {
        return (_attackPower);
    }
    
    public float MySightDistance()
    {
        return (_sightDistance);
    }
}
