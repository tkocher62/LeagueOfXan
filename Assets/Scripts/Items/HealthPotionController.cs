using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerController.singleton.health < 100f && collision.gameObject.tag == "Player")
        {
            PlayerController.singleton.Heal(10);
            Destroy(gameObject);
        }
    }
}
