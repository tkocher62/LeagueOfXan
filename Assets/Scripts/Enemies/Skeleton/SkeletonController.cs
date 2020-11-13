using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonController : Enemy
{
    public float damage;
    public float boneRange;
    public float meeleeInterval;
    public float rangedInterval;

    private GameObject prefab;
    private Animator animator;

    private bool isMeeleeCooldown;
    private bool isRangedCooldown;

    private void Start()
    {
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>(), GetComponent<AIDestinationSetter>());

        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/Bone");
        animator = GetComponent<Animator>();

        isMeeleeCooldown = false;
        isRangedCooldown = false;

        animator.SetBool("IsMoving", true);
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
        Assets.Scripts.General.Utils.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
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
