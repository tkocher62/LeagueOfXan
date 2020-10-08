using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampyController : Enemy
{
    public float safeDistance;
    public float graceDistance;
    public float attackInterval;

    private GameObject prefab;

    void Start()
    {
        Init(GetComponent<SpriteRenderer>(), GetComponent<Rigidbody2D>());

        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/SlimeBall");

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
        while (health > 0)
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
