using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DollPlayerDeathState : MonoBehaviour
{
    private StatsComponent stats;

    private DollPlayerModelView modelView;

    private BoxCollider box;

    private SphereCollider sphere;

    private SpriteRenderer rend;

    private DollPlayerMovement playerMovement;

    //how much rb spins
    [SerializeField] private float torque;

    //how much NPC "jumps" up on Death
    [SerializeField] private float verticalDist;

    //how far NPC travels horizontally
    //tie to hitDir
    [SerializeField] private float horizontalDist;

    //change direction depending on facing direction
    //change to direction of incoming hit?
    [SerializeField] private int hitDir;

    [SerializeField] private bool facingDir;

    private Gravity gravity;

    private void OnEnable()
    {
        playerMovement = GetComponent<DollPlayerMovement>();
        playerMovement.disabled = true;

        stats = GetComponent<StatsComponent>();
        stats.vulnerable = false;

        modelView = GetComponentInChildren<DollPlayerModelView>();

        modelView.OnDeath();

    }
}