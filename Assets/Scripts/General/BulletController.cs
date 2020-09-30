using Assets.Scripts.Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BulletController : MonoBehaviour
{
    private Renderer render;

    public float damage = 10f;

    void Start()
    {
        render = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (!render.isVisible) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy controller = collision.gameObject.GetComponent<Enemy>();
            if (controller != null)
            {
                controller.Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}
