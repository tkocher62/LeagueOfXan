using Assets.Scripts.Enemies;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedController : MonoBehaviour
{
    private float bleedAmount = 2f;
    internal const int totalBleedTicks = 3;
    private float bleedInterval = 1.5f;

    internal int curBleedTicks;

    void Start()
    {
        curBleedTicks = totalBleedTicks;

        Timing.RunCoroutine(Bleed().CancelWith(gameObject));
    }

    IEnumerator<float> Bleed()
    {
        Enemy controller = gameObject.GetComponent<Enemy>();
        for (int i = 0; i < curBleedTicks; i++)
        {
            yield return Timing.WaitForSeconds(bleedInterval);
            controller.Damage(bleedAmount);
        }
        Destroy(this);
    }
}
