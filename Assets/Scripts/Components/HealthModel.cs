using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class HealthModel : MonoBehaviour
{
    [SerializeField] private int maxHP;
    public int HP;

    private bool isAlive=true;

    private HealthModelView modelView;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<HealthModelView>();
        
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
                modelView.OnYouDied();
            }

            modelView.OnChangeHealth(HP);
        }
    }
}