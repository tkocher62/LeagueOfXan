using Assets.Scripts.General;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XanPlayerScript : PlayerScript
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

            Timing.CallDelayed(0.1f, () => Destroy(spray));

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
