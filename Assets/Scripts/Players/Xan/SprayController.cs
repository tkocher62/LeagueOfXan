using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    public float damage = 10f;

    private void Start()
    {
        Vector2 moveDirection = PlayerController.singleton.movement;

        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (moveDirection.x < 0f)
            {
                gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // prevent this from hurting enemies through walls
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy controller = collision.gameObject.GetComponent<Enemy>();
            if (controller != null)
            {
                controller.Damage(damage);
            }
        }
    }
}
