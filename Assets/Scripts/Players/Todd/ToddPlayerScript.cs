using Assets.Scripts.General;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToddPlayerScript : PlayerScript
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private float shootDelay = 1f;
    private bool isOnCooldown = false;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/SniperBullet");
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public override void Attack()
    {
        if (!isOnCooldown)
        {
            GameObject bullet = Utils.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            if (PlayerController.singleton.movement.normalized != Vector2.zero)
            {
                bullet.transform.up = PlayerController.singleton.movement.normalized;
                rb.AddForce(PlayerController.singleton.movement.normalized * 2000f);
            }
            else
            {
                Vector2 velocity = new Vector2(PlayerController.singleton.render.flipX ? -1 : 1, 0);
                bullet.transform.up = velocity;
                rb.AddForce(velocity * 2000f);
            }

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
