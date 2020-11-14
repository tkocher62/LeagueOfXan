using Assets.Scripts.General;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaileyPlayerScript : PlayerScript 
{
    private GameObject prefab;

    private float shootDelay = 3f;
    private bool isOnCooldown = false;

    private void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/BeeGrenade");
    }

    public override void Attack()
    {
        if (!isOnCooldown)
        {
            GameObject bee = Utils.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
