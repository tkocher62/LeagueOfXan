using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    public float damage = 10f;

    private Color color;

    private void Awake()
    {
        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.2f, 1f);
        GetComponent<SpriteRenderer>().color = color;
    }

    private void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<EdgeCollider2D>(), PlayerController.singleton.playerCollider, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyController controller = collision.gameObject.GetComponent<EnemyController>();
            if (controller != null)
            {
                controller.Damage(damage);
                controller.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}
