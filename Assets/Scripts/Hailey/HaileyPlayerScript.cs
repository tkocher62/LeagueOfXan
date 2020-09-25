using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaileyPlayerScript : MonoBehaviour
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private float shootDelay = 3f;
    private bool isOnCooldown = false;
    private Vector2 defaultPower = new Vector2(1, 0);

    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/Bee Grenade");
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Attack();
        }
    }

    internal void Attack()
    {
        if (!isOnCooldown)
        {
            GameObject bee = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
            Rigidbody2D rb = bee.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            bee.transform.up = playerBody.velocity.normalized;
            rb.drag = 5f;
            rb.angularDrag = 2f;
            rb.AddForce((playerBody.velocity.normalized != Vector2.zero ? playerBody.velocity.normalized : defaultPower) * 2200f);
            rb.AddTorque(100f);

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
