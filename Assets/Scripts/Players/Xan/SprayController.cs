﻿using Assets.Scripts.Enemies;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    public float damage = 7f;

    private SpriteRenderer render;

    private void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();

        Vector2 moveDirection = PlayerController.singleton.movement;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (moveDirection.x < 0f)
            {
                render.flipY = true;
            }
        }
        else
        {
            render.flipX = PlayerController.singleton.render.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RaycastHit2D raycast = Physics2D.Raycast(gameObject.transform.position, PlayerController.singleton.movement.normalized, 1f, 1 << 11);
        if (raycast.collider == null || raycast.collider.tag != "Screen Border" && collision.gameObject.tag == "Enemy")
        {
            Enemy controller = collision.gameObject.GetComponent<Enemy>();
            if (controller != null)
            {
                controller.Damage(damage);
            }
        }
    }
}
