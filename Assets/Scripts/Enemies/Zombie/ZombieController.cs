using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    public float movementSpeed;
    public float health;
    public float damage;

    private SpriteRenderer render;
    private Rigidbody2D rb;
    private float playerWidth, playerHeight;
    private bool isCooldown;

    private Vector2 movement;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        playerWidth = render.bounds.extents.x;
        playerHeight = render.bounds.extents.y;

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

        if (PlayerController.singleton.health > 0)
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
                Move(movement);
            }
        }
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

    private void Attack()
    {
        isCooldown = true;
        PlayerController.singleton.Damage(damage);
        Timing.CallDelayed(1f, () => isCooldown = false);
    }

    private void Move(Vector2 direction)
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
        rb.MovePosition((Vector2)viewPos + (direction * movementSpeed * Time.deltaTime));
    }
}
