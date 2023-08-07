using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int maxHP;
    public int HP;

    public bool isAlive = true;

    private HealthModelView modelView;

    [SerializeField] private float invincibilityTime;
    [SerializeField] public bool canTakeDamage;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<HealthModelView>();

        canTakeDamage = true;
        
        ChangeHP(maxHP);
    }

    public void ChangeHP(int amount)
    {
        if (isAlive && canTakeDamage)
        {
            HP += amount;

            if (amount < 0)
            {
                StartCoroutine(TakeDamageInvincibility());
            }

            if (HP >= maxHP)
                HP = maxHP;

            else if (HP <= 0)
            {
                HP = 0;
                isAlive = false;
                modelView.OnYouDied();
                canTakeDamage = false;
            }

            modelView.OnChangeHealth(HP);
        }
    }

    private IEnumerator TakeDamageInvincibility()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityTime);
        
        if(isAlive)
        canTakeDamage = true;
    }
}