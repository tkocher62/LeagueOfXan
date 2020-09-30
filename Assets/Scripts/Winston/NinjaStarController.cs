using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStarController : MonoBehaviour
{
    private Renderer render;

    public float damage = 4f;

    private void Start()
    {
        render = gameObject.GetComponent<Renderer>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Screen Border"))
        {
            Physics2D.IgnoreCollision(GetComponent<EdgeCollider2D>(), obj.GetComponent<Collider2D>(), true);
        }

        Physics2D.IgnoreCollision(GetComponent<EdgeCollider2D>(), PlayerController.singleton.playerCollider, true);
    }

    private void Update()
    {
        if (!render.isVisible) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy controller = collision.gameObject.GetComponent<Enemy>();
            if (controller != null)
            {
                BleedController bleedController = collision.gameObject.GetComponent<BleedController>();
                if (bleedController == null)
                {
                    collision.gameObject.AddComponent<BleedController>();
                    controller.Damage(damage);
                }
                else
                {
                    bleedController.curBleedTicks = bleedController.totalBleedTicks;
                }
                Destroy(gameObject);
            }
        }
    }
}
