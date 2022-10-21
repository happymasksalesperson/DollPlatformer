using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerStats : MonoBehaviour
{ 
    // // // // // //
    // DOLL STATS

    [SerializeField] private int maxHP;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxSpeed;
    
    public int MyMaxHP()
    {
        return maxHP;
    }
    
    public float MyRunSpeed()
    {
        return runSpeed;
    }
    
    public float MyMaxSpeed()
    {
        return maxSpeed;
    }
    
    public float MyJumpForce()
    {
        return jumpForce;
    }

}
