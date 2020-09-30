using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XanPlayerScript : MonoBehaviour
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private const float distFactor = 1.35f;
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
            GameObject spray = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);

            Vector2 moveDirection = playerBody.velocity;
            if (moveDirection != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                spray.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (moveDirection.x < 0f)
                {
                    spray.GetComponent<SpriteRenderer>().flipY = true;
                }
            }

            Timing.CallDelayed(0.1f, () => Destroy(spray));

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
