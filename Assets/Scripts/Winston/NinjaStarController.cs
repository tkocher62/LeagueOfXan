using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : MonoBehaviour
{
    private Renderer render;

    public float damage = 4f;

    void Start()
    {
        render = gameObject.GetComponent<Renderer>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Screen Border"))
        {
            Physics2D.IgnoreCollision(GetComponent<EdgeCollider2D>(), obj.GetComponent<Collider2D>(), true);
        }

        Physics2D.IgnoreCollision(GetComponent<EdgeCollider2D>(), PlayerController.singleton.playerCollider, true);
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
                BleedController bleedController = collision.gameObject.GetComponent<BleedController>();
                if (bleedController == null)
                {
                    collision.gameObject.AddComponent<BleedController>();
                }
                else
                {
                    bleedController.curBleedTicks = BleedController.totalBleedTicks;
                }
                Destroy(gameObject);
            }
        }
    }
}
