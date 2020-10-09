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
    private AIDestinationSetter ai;
    private bool isCooldown;
    private int wallMask;
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
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>(), GetComponent<AIDestinationSetter>());

        animator = GetComponent<Animator>();
        ai = GetComponent<AIDestinationSetter>();

        wallMask = LayerMask.NameToLayer("Obstacle");

        isAgro = false;
        isCooldown = false;
    }

    private void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position);

        if (dist < detectionRange && Physics2D.Linecast(transform.position, PlayerController.singleton.gameObject.transform.position, wallMask))
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
