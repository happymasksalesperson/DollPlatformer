using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamewheelShoot : MonoBehaviour
{
    public bool isPlayer;

    public Transform target;

    public FlameWheel flameWheel;

    public ObjectPool pool;
    

    public void ShootFireball()
    {
        GameObject fireObj = pool.GetPooledObject();

        FireballBrain fireball = fireObj.GetComponent<FireballBrain>();

        fireball.ChangeState(FireballBrain.FireballStateEnum.Projectile);
    }
}
