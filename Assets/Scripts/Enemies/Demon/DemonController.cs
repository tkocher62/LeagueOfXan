using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : Enemy
{
    public float movementSpeed;
    public float detectionRange;
    public float attackInterval;
    public float damage;

    private Animator animator;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private float playerWidth, playerHeight;
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

    private Vector2 movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        playerWidth = render.bounds.extents.x;
        playerHeight = render.bounds.extents.y;

        isAgro = false;
        isCooldown = false;
    }

    private void Update()
    {
        movement = (PlayerController.singleton.gameObject.transform.position - transform.position).normalized;

        if (movement != Vector2.zero)
        {
            render.flipX = movement.x < 0;
        }
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
            else
            {
                Move(rb, movement, playerWidth, playerHeight, movementSpeed);
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
