using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerStats : MonoBehaviour, ITakeDamage
{
    // // // // // //
    // DOLL STATS

    [Header("HEALTH POINTS & VULNERABLE STATUS")]
    public int maxHP = 3;

    public int HP;

    public bool vulnerable = true;

    public bool armoured = false;

    [Header("RUN SPEED")] public float runSpeed;
    public float maxSpeed;

    [Header("JUMP VERTICAL POWER")] public float jumpForce;

    [Header("Horizontal Jump Speed Multiplier (0.5 sets speed to half)")]
    public float jumpSpeedMultipler;

    [Header("ATTACK01 POWER")] public int attack01Power;

    [Header("ATTACK01 RADIUS SIZE")] public float attack01Radius;

    [Header("ATTACK01 TIME VALUES")] public float attack01Time;
    public float attack02Time;

    [Header("JUMP ATTACK TIMES")] public float jumpAttack01Time;

    [Header("TAKE DAMAGE TIME AND SUBSEQUENT GRACE PERIOD")] [SerializeField]
    public float takeDamageTime;

    [SerializeField] public float takeDamageGracePeriod;

    private StateManager stateManager;

    // // // // // //
    // 

    private HealthModelView modelView;

    // // // // // //
    // 

    private void Start()
    {
        modelView = GetComponentInChildren<HealthModelView>();

        stateManager = GetComponent<StateManager>();

        HP = maxHP;
    }

    public void ChangeHP(int amount)
    {
        if (vulnerable)
        {
            //negative / take damage / die
            if (amount < 0)
            {
                if (HP + amount <= 0)
                {
                    HP = 0;
                }
                else
                {
                    HP += amount;
                }

                if(!armoured && HP>0)
                    stateManager.ChangeStateString("takeDamage");
                else if (HP <= 0)
                {
                    stateManager.ChangeStateString("death");
                }
            }
        }
        //healed
        else if (amount > 0)
        {
            if (HP + amount >= maxHP)
                HP = maxHP;
            else
                HP += amount;
        }
        modelView.OnChangeHealth(amount);
        Debug.Log("HP: "+HP);
    }
}