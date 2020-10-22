using Assets.Scripts.General;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaileyPlayerScript : PlayerScript 
{
    private GameObject prefab;
    private Rigidbody2D playerBody;

    private float shootDelay = 3f;
    private bool isOnCooldown = false;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/BeeGrenade");
        playerBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        RaycastHit2D raycast = Physics2D.Raycast(gameObject.transform.position, PlayerController.singleton.movement.normalized, 1f, 1 << 11);
        if (!isOnCooldown)
        {
            GameObject bee = Instantiate(prefab, gameObject.transform.position, Quaternion.identity);

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
