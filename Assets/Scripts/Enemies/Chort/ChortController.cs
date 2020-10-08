using Assets.Scripts.Enemies;
using MEC;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChortController : Enemy
{
    public float detectionRange;
    public float attackInterval;
    public float damage;

    private Animator animator;
    private AIDestinationSetter ai;
    private bool isCooldown;
    private bool _isAgro;
    private bool isAgro
    {
        get
        {
            return _isAgro;
        }
        set
        {
            if (animator != null) animator.SetBool("IsMoving", value);
            _isAgro = value;
            ai.target = value ? PlayerController.singleton.transform : null;
        }
    }

    private void Start()
    {
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>());

        animator = GetComponent<Animator>();
        ai = GetComponent<AIDestinationSetter>();

        isAgro = false;
        isCooldown = false;
    }

    private void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position);

        if (dist < detectionRange)
        {
            isAgro = true;
        }

        if (PlayerController.singleton.health > 0 && isAgro)
        {
            if (dist <= playerWidth || dist < playerHeight)
            {
                if (!isCooldown)
                {
                    Attack();
                }
            }
        }
    }

    private void Attack()
    {
        isCooldown = true;
        PlayerController.singleton.Damage(damage);
        Timing.CallDelayed(attackInterval, () => isCooldown = false);
    }
}
