using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonController : MonoBehaviour
{
    internal float poisonAmount = 0.35f;
    internal int totalPoisonTicks = 30;
    internal float poisonInterval = 0.15f;

    internal int curPoisonTicks;

    Color color = Color.green;

    private void Start()
    {
        curPoisonTicks = totalPoisonTicks;

        PlayerController.singleton.render.color = color;

        Timing.RunCoroutine(Poison().CancelWith(gameObject));
    }

    private IEnumerator<float> Poison()
    {
        for (int i = 0; i < totalPoisonTicks; i++)
        {
            PlayerController.singleton.Damage(poisonAmount, false);
            yield return Timing.WaitForSeconds(poisonInterval);
        }
        PlayerController.singleton.render.color = Color.white;
        Destroy(this);
    }
}
