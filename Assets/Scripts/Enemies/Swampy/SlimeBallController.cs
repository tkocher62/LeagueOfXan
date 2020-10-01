using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBallController : MonoBehaviour
{
    public float damage;

    private SpriteRenderer render;

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();

        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.angularDrag = 0f;
        rb.AddForce((PlayerController.singleton.gameObject.transform.position - gameObject.transform.position).normalized * 300f);
    }

    private void Update()
    {
        if (!render.isVisible) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PoisonController poisonController = collision.gameObject.GetComponent<PoisonController>();
            if (poisonController == null)
            {
                collision.gameObject.AddComponent<PoisonController>();
            }
            else
            {
                poisonController.curPoisonTicks = poisonController.totalPoisonTicks;
            }
            Destroy(gameObject);
        }
    }
}
