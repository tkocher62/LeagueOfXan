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
    public float health;

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
                Move(movement);

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

    private void Move(Vector2 direction)
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
        rb.MovePosition((Vector2)viewPos + (direction * movementSpeed * Time.deltaTime));
    }

    private void MeeleeAttack()
    {
        PlayerController.singleton.Damage(damage);
        isMeeleeCooldown = true;
        Timing.CallDelayed(meeleeInterval, () => isMeeleeCooldown = false);
    }

    public override void Damage(float damage)
    {
        health -= damage;
        FlashRed();
        if (health <= 0f)
        {
            Destroy(gameObject);
            Kill();
        }
    }
}
