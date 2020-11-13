using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Assets.Scripts.Enemies;

public class NecromancerController : Enemy
{
    public float safeDistance;
    public float graceDistance;
    public float attackInterval;

    private GameObject prefab;

    private void Start()
    {
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>());

        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MagicBall");

        Timing.RunCoroutine(AttackCoroutine().CancelWith(gameObject));
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
        while (health > 0 && this)
        {
            Attack();
            yield return Timing.WaitForSeconds(attackInterval);
        }
    }

    private void Attack()
    {
        Assets.Scripts.General.Utils.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    }
}
