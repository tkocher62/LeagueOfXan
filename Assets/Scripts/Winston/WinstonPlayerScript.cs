using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class WinstonPlayerScript : MonoBehaviour
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private float shootDelay = 1f;
    private bool isOnCooldown = false;

    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/NinjaStar");
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isOnCooldown)
        {
            if (Input.GetKeyDown("f"))
            {
                Attack();
            }
        }
    }

    private Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    internal void Attack()
    {
        isOnCooldown = true;
        for (int i = -1; i < 2; i++)
        {
            GameObject ninjastar = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            Rigidbody2D rb = ninjastar.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            ninjastar.transform.up = playerBody.velocity.normalized;
            Vector2 dir2 = (playerBody.velocity + Vector2FromAngle(45 * i)).normalized;
            rb.AddForce(dir2 * 1000f);
        }
        Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
    }
}
