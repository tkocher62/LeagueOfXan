using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Assets.Scripts.Enemies;

public class NecromancerController : Enemy
{
    public float movementSpeed;
    public float safeDistance;
    public float graceDistance;
    public float attackInterval;
    public float health;

    private GameObject prefab;
    private SpriteRenderer render;
    private Rigidbody2D rb;
    private float playerWidth, playerHeight;

    private Vector2 movement;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MagicBall");

        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        playerWidth = render.bounds.extents.x;
        playerHeight = render.bounds.extents.y;

        Timing.RunCoroutine(AttackCoroutine().CancelWith(gameObject));
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
            if (dist > safeDistance)
            {
                Move(movement);
            }
            else if (dist < safeDistance - graceDistance)
            {
                Move(-movement);
            }
        }
    }

    private IEnumerator<float> AttackCoroutine()
    {
        while (health > 0)
        {
            Attack();
            yield return Timing.WaitForSeconds(attackInterval);
        }
    }

    private void Move(Vector2 direction)
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
        rb.MovePosition((Vector2)viewPos + (direction * movementSpeed * Time.deltaTime));

    }

    private void Attack()
    {
        Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
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
