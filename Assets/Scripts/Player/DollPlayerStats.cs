using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerStats : MonoBehaviour
{ 
    // // // // // //
    // DOLL STATS

    [SerializeField] private int HP;
    [SerializeField] private int maxHP;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;

    public int MyHP()
    {
        return HP;
    }
    
    public int MyMaxHP()
    {
        return maxHP;
    }
    
    public float MyRunSpeed()
    {
        return runSpeed;
    }
    
    public float MyJumpForce()
    {
        return jumpForce;
    }

}
