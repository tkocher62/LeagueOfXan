using Assets.Scripts.Enemies;
using MEC;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : Enemy
{
    public float detectionRange;
    public float attackInterval;
    public float damage;

    private Animator animator;
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
        }
    }

    private void Start()
    {
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>(), GetComponent<AIDestinationSetter>());

        animator = GetComponent<Animator>();

        isAgro = false;
        isCooldown = false;
    }

    private void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position);

        if (dist < detectionRange && Physics2D.Linecast(transform.position, PlayerController.singleton.transform.position, wallMask, 0f, detectionRange))
        {
            isAgro = true;
        }

        if (PlayerController.singleton.health > 0 && isAgro && (dist <= playerWidth || dist < playerHeight) && !isCooldown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        isCooldown = true;
        PlayerController.singleton.Damage(damage);
        Timing.CallDelayed(attackInterval, () => isCooldown = false);
    }
}
