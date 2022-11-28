using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class HealthModel : MonoBehaviour
{
    [SerializeField] private int maxHP;
    public int HP;

    private bool isAlive=true;

    public HealthModelView modelView;

    private void OnEnable()
    {
        ChangeHP(maxHP);
    }

    public void ChangeHP(int amount)
    {
        if (isAlive)
        {
            HP += amount;

            if (HP >= maxHP)
                HP = maxHP;

            if (HP <= 0)
            {
                HP = 0;
                isAlive = false;
            }

            modelView.OnChangeHealth(HP);
        }
    }

    public float GetHP()
    {
        return HP;
    }
}