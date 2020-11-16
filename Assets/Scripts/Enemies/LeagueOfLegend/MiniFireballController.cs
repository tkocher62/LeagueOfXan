using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFireballController : MonoBehaviour
{
    private const float damage = 4f;

    private void Start()
    {
        LeagueOfLegendController.singleton.projectiles.Add(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (LeagueOfLegendController.singleton.projectiles.Contains(gameObject))
        {
            LeagueOfLegendController.singleton.projectiles.Remove(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController.singleton.Damage(damage);
            Destroy(gameObject);
        }
    }
}
