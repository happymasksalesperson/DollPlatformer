using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamewheelShoot : MonoBehaviour
{
    public bool isPlayer;

    public Transform target;

    public FlameWheel flameWheel;

    public List<GameObject> fireballs;

    public void SetFireballs()
    {
        fireballs = flameWheel.listFireballs;
    }

    public void ShootFireball()
    {

    }
}
