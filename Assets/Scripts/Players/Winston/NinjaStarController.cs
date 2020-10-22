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
                controller.Damage(damage);
                BleedController bleedController = collision.gameObject.GetComponent<BleedController>();
                if (bleedController == null)
                {
                    collision.gameObject.AddComponent<BleedController>();
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
