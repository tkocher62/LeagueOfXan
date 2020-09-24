﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Renderer render;

    public float damage = 10f;

    void Start()
    {
        render = gameObject.GetComponent<Renderer>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Screen Border"))
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), obj.GetComponent<BoxCollider2D>(), true);
        }
    }

    void Update()
    {
        if (!render.isVisible) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController controller = collision.gameObject.GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}
