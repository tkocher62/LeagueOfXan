using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedController : MonoBehaviour
{
    internal float bleedAmount = 2f;
    internal int totalBleedTicks = 3;
    internal float bleedInterval = 1.5f;

    internal int curBleedTicks;

    private void Start()
    {
        curBleedTicks = totalBleedTicks;

        Timing.RunCoroutine(Bleed().CancelWith(gameObject));
    }

    private IEnumerator<float> Bleed()
    {
        Enemy controller = gameObject.GetComponent<Enemy>();
        for (int i = 0; i < curBleedTicks; i++)
        {
            yield return Timing.WaitForSeconds(bleedInterval);
            if (controller != null)
            {
                controller.Damage(bleedAmount);
            }
            else yield break;
        }
        Destroy(this);
    }
}
