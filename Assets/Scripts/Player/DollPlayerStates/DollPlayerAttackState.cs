using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollPlayerAttackState : MonoBehaviour
{
    private DollPlayerStats stats;

    private bool attacking = true;

    private float attack01Time;

    private int attackPower;

    [SerializeField] private Vector3 attackCenter;

    [SerializeField] private Vector3 Attack01Box;

    private void Start()
    {
        stats = GetComponent<DollPlayerStats>();
        attackPower = stats.attackPower;
        attack01Time = stats.attack01Time;
    }

    private void OnEnable()
    {
        attacking = true;
        StartCoroutine(GroundAttack01());
    }

    private IEnumerator GroundAttack01()
    {
        while (attacking)
        {
            attackCenter = transform.position;

            Collider[] hitColliders =
                Physics.OverlapBox(attackCenter, Attack01Box, Quaternion.identity, 9999, QueryTriggerInteraction.Collide);
            foreach (var hitCollider in hitColliders)
            {
                //GameObject fire = Instantiate(_fire01Prefab, transform.position, Quaternion.identity) as GameObject;

                if (hitCollider.GetComponent<HealthModel>() != null)
                {
                    hitCollider.GetComponent<HealthModel>().ChangeHP(attackPower);
                }
            }

            yield return new WaitForSeconds(attack01Time);
        }

        if (!attacking)
        {
            //change state;
        }
    }
}