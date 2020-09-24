﻿using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackPlayerScript : MonoBehaviour
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private float shootDelay = 0.2f;
    private bool isOnCooldown = false;

    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/Laser");
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isOnCooldown)
        {
            if (Input.GetKey("f"))
            {
                Attack();
            }
        }
    }

    internal void Attack()
    {
        GameObject bullet = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        bullet.transform.up = playerBody.velocity.normalized;
        rb.AddForce(playerBody.velocity.normalized * 1000f);

        isOnCooldown = true;
        Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
    }
}
