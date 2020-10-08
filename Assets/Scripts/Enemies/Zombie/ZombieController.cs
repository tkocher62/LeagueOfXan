using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    public float attackInterval;
    public float damage;

    private bool isCooldown;

    private void Start()
    {
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>());

        isCooldown = false;
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

    private void Attack()
    {
        isCooldown = true;
        PlayerController.singleton.Damage(damage);
        Timing.CallDelayed(attackInterval, () => isCooldown = false);
    }
}
