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
                Move(rb, movement, playerWidth, playerHeight, movementSpeed);
            }
            else if (dist < safeDistance - graceDistance)
            {
                Move(rb, -movement, playerWidth, playerHeight, movementSpeed);
            }
        }
    }

    private IEnumerator<float> AttackCoroutine()
    {
        while (health > 0 && this)
        {
            Attack();
            yield return Timing.WaitForSeconds(attackInterval);
        }
    }

    private void Attack()
    {
        Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    }
}
