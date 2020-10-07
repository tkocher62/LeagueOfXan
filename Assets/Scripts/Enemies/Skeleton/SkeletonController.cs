using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : Enemy
{
    public float movementSpeed;
    public float damage;
    public float boneRange;
    public float meeleeInterval;
    public float rangedInterval;

    private GameObject prefab;
    private Animator animator;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private float playerWidth, playerHeight;

    private bool isMeeleeCooldown;
    private bool isRangedCooldown;

    private Vector2 movement;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/Bone");
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        playerWidth = render.bounds.extents.x;
        playerHeight = render.bounds.extents.y;

        isMeeleeCooldown = false;
        isRangedCooldown = false;

        animator.SetBool("IsMoving", true);
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

        if (PlayerController.singleton.health > 0)
        {
            if (dist <= playerWidth || dist < playerHeight)
            {
                if (!isMeeleeCooldown)
                {
                    MeeleeAttack();
                }
            }
            else
            {
                Move(rb, movement, playerWidth, playerHeight, movementSpeed);

                if (dist > boneRange)
                {
                    if (!isRangedCooldown)
                    {
                        RangedAttack();
                    }
                }
            }
        }
    }

    private void RangedAttack()
    {
        Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        isRangedCooldown = true;
        Timing.CallDelayed(rangedInterval, () => isRangedCooldown = false);
    }

    private void MeeleeAttack()
    {
        PlayerController.singleton.Damage(damage);
        isMeeleeCooldown = true;
        Timing.CallDelayed(meeleeInterval, () => isMeeleeCooldown = false);
    }
}
