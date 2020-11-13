using Assets.Scripts.General;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies.LeagueOfLegend
{
	class FireballController : MonoBehaviour
	{
        private GameObject prefab;

        private float distFromPlayer;
        private float explodeDist;

        private Transform playerLoggedPos;

        private const float triggerDistance = 0.7f;
        private const int fireballCount = 12;

        private void Start()
        {
            prefab = Resources.Load<GameObject>("Prefabs/Projectiles/MiniFireball");

            distFromPlayer = Vector2.Distance(gameObject.transform.position, PlayerController.singleton.gameObject.transform.position);
            explodeDist = -1f;

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;

            Vector3 targ = PlayerController.singleton.gameObject.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 180));

            rb.AddForce((PlayerController.singleton.transform.position - gameObject.transform.position).normalized * 500f);
            playerLoggedPos = PlayerController.singleton.transform;

            explodeDist = Random.Range(2f, distFromPlayer * triggerDistance);
        }

        // todo: add damage if fireball hits player during travel time

        private void Update()
        {
            float dist = Vector2.Distance(gameObject.transform.position, playerLoggedPos.position);

            float t = dist - explodeDist;
            Debug.Log(t);
            if (t < 0f)
            {
                // todo: deal base damage in a radius
                Destroy(gameObject);
                Explode();
            }
        }

        private void Explode()
        {
            for (int i = 0; i < fireballCount; i++)
            {
                float inc = 360 / fireballCount * i;
                Vector2 angle = Utils.Vector2FromAngle(inc);
                GameObject fireball = Instantiate(prefab, gameObject.transform.position, transform.rotation * Quaternion.Euler(0, 0, -180 + (inc)));
                Rigidbody2D rb = fireball.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0f;

                rb.AddForce(angle.normalized * 300f);
            }
        }
    }
}
