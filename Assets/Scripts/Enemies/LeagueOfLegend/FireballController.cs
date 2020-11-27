using Assets.Scripts.General;
using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies.LeagueOfLegend
{
	class FireballController : MonoBehaviour
	{
        private GameObject prefab;

        private float distFromPlayer;
        private float explodeDist;
        private float lastDist;
        private float distanceToTravel;

        private Transform playerLoggedPos;

        private const float triggerDistance = 0.7f;
        private const int fireballCount = 12;
        private const float hitDamage = 30;

        private bool isOnCooldown;

        private void Start()
        {
            prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MiniFireball");

            distFromPlayer = Vector2.Distance(gameObject.transform.position, PlayerController.singleton.gameObject.transform.position);
            explodeDist = -1f;

            isOnCooldown = false;

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;

            Vector3 targ = PlayerController.singleton.gameObject.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 180));

            playerLoggedPos = PlayerController.singleton.transform;
            rb.AddForce((playerLoggedPos.position - gameObject.transform.position).normalized * 500f);

            explodeDist = Random.Range(2f, distFromPlayer * triggerDistance);
            lastDist = Vector2.Distance(gameObject.transform.position, playerLoggedPos.position);
            distanceToTravel = Vector2.Distance(gameObject.transform.position, playerLoggedPos.position) - explodeDist;

            SfxController.singleton.fireball.Play();
        }

        private void Update()
        {
            float dist = Vector2.Distance(gameObject.transform.position, playerLoggedPos.position);

            distanceToTravel -= Mathf.Abs(lastDist - dist);
            if (distanceToTravel < 0f)
            {
                Destroy(gameObject);
                Explode();
            }

            lastDist = dist;
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
            if (collision.gameObject.tag == "Player" && !isOnCooldown)
            {
                PlayerController.singleton.Damage(hitDamage);
                Timing.RunCoroutine(Cooldown().CancelWith(gameObject));
            }
        }

        private IEnumerator<float> Cooldown()
        {
            isOnCooldown = true;
            yield return Timing.WaitForSeconds(1f);
            isOnCooldown = false;
        }

        private void Explode()
        {
            for (int i = 0; i < fireballCount; i++)
            {
                float inc = 360 / fireballCount * i;
                Vector2 angle = Utils.Vector2FromAngle(inc);
                GameObject fireball = Instantiate(prefab, gameObject.transform.position, Quaternion.identity * Quaternion.Euler(0, 0, -180 + inc));
                Rigidbody2D rb = fireball.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0f;

                rb.AddForce(angle.normalized * 300f);
            }
            LeagueOfLegendController.singleton.render.sprite = LeagueOfLegendController.singleton.s_Neutral;
            SfxController.singleton.fireballExplosion.Play();
            Timing.RunCoroutine(Utils.CallDelayed(1.3f, () => LeagueOfLegendController.singleton.Attack()).CancelWith(LeagueOfLegendController.singleton.gameObject));
        }
    }
}
