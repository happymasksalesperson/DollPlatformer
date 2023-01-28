using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectDeath : MonoBehaviour
{
    private StatsComponent stats;

    private HealthModelView modelView;

    private Rigidbody rb;

    private SpriteRenderer rend;
    
    //how far NPC travels horizontally
    //tie to hitDir
    [SerializeField] private float horizontalDist;

    //how much object"jumps" up on Death
    [SerializeField] private float verticalDist;

    private Gravity gravity;

    private void OnEnable()
    {
        stats = GetComponent<StatsComponent>();

        modelView = GetComponentInChildren<HealthModelView>();

        modelView.OnYouDied();

        rb = GetComponent<Rigidbody>();

        gravity = GetComponent<Gravity>();

        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionZ;

        rb.AddForce(new Vector3(horizontalDist, verticalDist, 0), ForceMode.Impulse);

        gravity.enabled = true;
        StartCoroutine(Die());
    }

    //destroys gameobject once renderer is out of sight
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(5);

        if (!rend.isVisible)
        {
            LevelManager.levelManager.SFX.Unsubscribe(gameObject);
            Destroy(gameObject);
        }

        else StartCoroutine(Die());
    }
}