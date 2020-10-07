using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneController : MonoBehaviour
{
    public float damage;

    private SpriteRenderer render;

    private void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.angularDrag = 0f;
        rb.AddTorque(500f);
        rb.AddForce((PlayerController.singleton.gameObject.transform.position - gameObject.transform.position).normalized * 400f);
    }

    private void Update()
    {
        if (!render.isVisible) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.singleton.Damage(damage);
            Destroy(gameObject);
        }
    }
}
