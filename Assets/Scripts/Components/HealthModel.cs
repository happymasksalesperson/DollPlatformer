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

    public float invincibilityTime;
    public bool canTakeDamage;

    public int testAmount;

    private void OnEnable()
    {
        modelView = GetComponentInChildren<HealthModelView>();

        canTakeDamage = true;
        
        ChangeHP(maxHP);
    }

    public void Resurrect()
    {
        isAlive = true;
        ChangeHP(maxHP);
    }

    public void Death()
    {
        ChangeHP(-maxHP);
    }

    public void TestChangeHealth()
    {
        ChangeHP(testAmount);
    }

    public void ChangeHP(int amount)
    {
        if (isAlive && canTakeDamage)
        {
            HP += amount;

            if (amount < 0)
            {
                modelView.OnChangeInvincibilityState(true);
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

        modelView.OnChangeInvincibilityState(false);
    }
}