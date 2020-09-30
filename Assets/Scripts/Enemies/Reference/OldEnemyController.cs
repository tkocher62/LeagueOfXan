using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemyController : MonoBehaviour
{
    public Animator animator;

    private SpriteRenderer render;
    private Rigidbody2D rb;

    //public GameObject healthBar;

    private float health = 20f;
    private float detectionRange = 5f;
    private float movementSpeed = 1f;
    private float attackRange = 1.3f;
    private float attackCooldown = 1.5f;

    private float damage = 50f;

    private bool isAgro = false;
    private bool isOnCooldown = false;

    private Vector2 movement;

    private Color defaultColor;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        defaultColor = render.color;
    }

    void Update()
    {
        movement = (PlayerController.singleton.gameObject.transform.position - transform.position).normalized;

        float dist = Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position);
        if (dist < detectionRange)
        {
            isAgro = true;
            if (animator != null) animator.SetBool("IsMoving", true);
        }

        if (movement != Vector2.zero)
        {
            render.flipX = movement.x < 0;
        }

        if (isAgro && !isOnCooldown)
        {
            if (dist < attackRange)
            {
                Attack();
                isOnCooldown = true;
                Timing.CallDelayed(attackCooldown, () => isOnCooldown = false);
            }
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

    void Attack()
    {
        PlayerController.singleton.health -= damage;
    }

    public void Damage(float amount)
    {
        if (!isAgro)
        {
            isAgro = true;
        }

        health -= amount;
        Timing.RunCoroutine(FlashRed().CancelWith(gameObject));
        if (health <= 0)
        {
            Destroy(gameObject);
            MapController.singleton.enemies--;
        }
    }

    IEnumerator<float> FlashRed()
    {
        render.color = new Color(255, 0, 0);
        yield return Timing.WaitForSeconds(0.2f);
        render.color = defaultColor;
    }
}
