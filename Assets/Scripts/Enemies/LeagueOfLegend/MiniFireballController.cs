using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFireballController : MonoBehaviour
{
    private const float damage = 4f;

    private void Start()
    {
        /*Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        rb.AddForce(gameObject.transform.position.normalized * 750f);*/
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
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
