using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using Assets.Scripts.General;

public class WinstonPlayerScript : PlayerScript
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private float shootDelay = 1f;
    private bool isOnCooldown = false;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/NinjaStar");
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Attack();
        }
    }

    private Vector2 Vector2FromAngle(float a)
    {
        a *= Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
    }

    public override void Attack()
    {
        if (!isOnCooldown)
        {
            for (int i = -1; i < 2; i++)
            {
                GameObject ninjastar = Utils.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
                Rigidbody2D rb = ninjastar.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0f;

                ninjastar.transform.up = PlayerController.singleton.movement.normalized != Vector2.zero ? PlayerController.singleton.movement.normalized : (PlayerController.singleton.render.flipX ? Vector2.left : Vector2.right);
                rb.AddForce((PlayerController.singleton.movement + (Vector2FromAngle(45 * i) * (PlayerController.singleton.render.flipX ? -1 : 1))).normalized * 800f);
            }
            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
