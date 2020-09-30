using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    public float health;
    public float detectionRange;
    public float movementSpeed;

    private SpriteRenderer render;
    private Rigidbody2D rb;

    private bool isAgro = false;
    private Vector2 movement;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = (PlayerController.singleton.gameObject.transform.position - transform.position).normalized;

        float dist = Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position);
        if (dist < detectionRange)
        {
            isAgro = true;
        }

        if (movement != Vector2.zero)
        {
            render.flipX = movement.x < 0;
        }
    }

    private void FixedUpdate()
    {
        if (PlayerController.singleton.health > 0 && isAgro)
        {
            Move(movement);
        }
    }

    private void Move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * movementSpeed * Time.deltaTime));
    }
}
