using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerStats : MonoBehaviour, ITakeDamage
{ 
    // // // // // //
    // DOLL STATS

    [Header("HEALTH POINTS")]
    public int maxHP;
    private int HP;

    [Header("RUN SPEED")]
    public float runSpeed;
    public float maxSpeed;
    
    [Header("HP")]
    public float jumpForce;
    
    [Header("ATTACK POWER")]
    public int attackPower;
    
    [Header("ATTACK TIME VALUES")]
    public float attack01Time;
    public float attack02Time;
    
    public float jumpAttack01Time;
    
    // // // // // //
    // 

    [SerializeField] private HealthModelView modelView;
    
    // // // // // //
    // 

    private void OnEnable()
    {
        modelView = GetComponentInChildren<HealthModelView>();
    }

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
    
}
