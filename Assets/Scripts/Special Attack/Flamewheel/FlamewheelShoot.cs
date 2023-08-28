using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamewheelShoot : MonoBehaviour
{
    public Transform target;

    public FlameWheel flameWheel;

    public ObjectPool pool;

    public void ShootFireball(GameObject fireballObj)
    {
        fireballObj.transform.SetParent(null);

        FireballBrain fireballBrain = fireballObj.GetComponent<FireballBrain>();
        fireballBrain.targetTransform = target;
        fireballBrain.shootPointTransform = transform;
        fireballBrain.ChangeState(FireballBrain.FireballStateEnum.Projectile);
    }
}
