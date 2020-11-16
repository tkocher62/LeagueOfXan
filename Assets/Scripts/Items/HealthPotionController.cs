using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    internal static float amount = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.singleton.health < 100f && collision.gameObject.tag == "Player")
        {
            PlayerController.singleton.Heal(amount);
            Destroy(gameObject);
        }
    }
}
