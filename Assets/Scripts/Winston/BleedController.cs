using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedController : MonoBehaviour
{
    private float bleedAmount = 2f;
    internal const int totalBleedTicks = 3;
    internal int curBleedTicks = 3;
    private float bleedInterval = 1.5f;

    void Start() => Timing.RunCoroutine(Bleed().CancelWith(gameObject));

    IEnumerator<float> Bleed()
    {
        EnemyController controller = gameObject.GetComponent<EnemyController>();
        for (int i = 0; i < curBleedTicks; i++)
        {
            yield return Timing.WaitForSeconds(bleedInterval);
            controller.Damage(bleedAmount);
        }
        Destroy(this);
    }
}
