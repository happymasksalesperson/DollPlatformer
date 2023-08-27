using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamewheelShoot : MonoBehaviour
{
    public bool isPlayer;

    public Transform target;

    public FlameWheel flameWheel;

    public ObjectPool pool;
    

    public void ShootFireball(GameObject fireballObj)
    {
        FireballBrain fireballBrain = fireballObj.GetComponent<FireballBrain>();
        fireballBrain.targetTransform = target;
        fireballBrain.shootPointTransform = transform;
        fireballBrain.ChangeState(FireballBrain.FireballStateEnum.Projectile);
    }
}
