using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer render;

    //public GameObject healthBar;

    private float health = 20f;
    private float detectionRange = 5f;
    //private float step = 0.006f;
    private float attackRange = 1.3f;
    private float attackCooldown = 1.5f;

    private float damage = 50f;

    private bool isAgro = false;
    private bool isOnCooldown = false;

    private Color defaultColor;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        render = GetComponent<SpriteRenderer>();

        defaultColor = render.color;

        //Instantiate(healthBar, transform.position, transform.rotation);
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < detectionRange)
        {
            isAgro = true;
        }

        if (isAgro && !isOnCooldown)
        {
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            if (dist < attackRange)
            {
                Attack();
                isOnCooldown = true;
                Timing.CallDelayed(attackCooldown, () => isOnCooldown = false);
            }
        }
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
