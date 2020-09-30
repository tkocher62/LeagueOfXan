using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeGrenadeController : MonoBehaviour
{
    private GameObject explosionPrefab;
    private Rigidbody2D body;
    private SpriteRenderer render;

    private bool isExploding = false;

    private float damageScale = 40f;

    private Vector3 lastFrameVelocity;

    void Start()
    {
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Effects/Explosion");
        body = gameObject.GetComponent<Rigidbody2D>();
        render = gameObject.GetComponent<SpriteRenderer>();

        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), PlayerController.singleton.playerCollider, true);
    }

    private void Update()
    {
        lastFrameVelocity = body.velocity;
        if (!isExploding && body.velocity.magnitude < 0.15f && body.velocity.magnitude != 0f)
        {
            Explode();
            isExploding = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Screen Border")
        {
            body.velocity = Vector3.Reflect(lastFrameVelocity.normalized, collision.contacts[0].normal) * lastFrameVelocity.magnitude;
        }
    }

    void Explode()
    {
        render.enabled = false;
        GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float dist = Vector2.Distance(gameObject.transform.position, obj.transform.position);
            if (dist < 3.5f)
            {
                Enemy controller = obj.GetComponent<Enemy>();
                if (controller != null)
                {
                    Debug.Log("distance: " + dist);
                    Debug.Log("damage: " + (1 / (transform.position - obj.transform.position).sqrMagnitude * damageScale));
                    controller.Damage(1 / (transform.position - obj.transform.position).sqrMagnitude * damageScale);
                }
            }
        }
        Timing.CallDelayed(0.3f, () =>
        {
            Destroy(explosion);
            Destroy(gameObject);
        });
    }
}
