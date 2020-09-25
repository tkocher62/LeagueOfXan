using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XanPlayerScript : MonoBehaviour
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private const float distMultiplier = 1.75f;
    private float shootDelay = 1f;
    private bool isOnCooldown = false;

    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Effects/Spray");
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
            Vector3 pos = gameObject.transform.position;
            Vector3 factor = playerBody.velocity.normalized * distMultiplier;
            pos.x += factor.x;
            pos.y += factor.y;
            GameObject spray = Instantiate(prefab, pos, Quaternion.identity);
            Rigidbody2D rb = spray.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            spray.transform.up = -playerBody.velocity.normalized;

            Timing.CallDelayed(0.1f, () => Destroy(spray));

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
