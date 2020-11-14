using Assets.Scripts.General;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XanPlayerScript : PlayerScript
{
    private GameObject prefab;
    private float shootDelay = 1f;
    private bool isOnCooldown = false;

    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/Effects/Spray");
    }

    public override void Attack()
    {
        if (!isOnCooldown)
        {
            GameObject spray = Utils.Instantiate(prefab, gameObject.transform.position, Quaternion.identity);

            Timing.CallDelayed(0.1f, () => Destroy(spray));

            isOnCooldown = true;
            Timing.CallDelayed(shootDelay, () => isOnCooldown = false);
        }
    }
}
