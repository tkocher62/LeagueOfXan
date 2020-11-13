using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies.LeagueOfLegend
{
	class FireballController : MonoBehaviour
	{
        private float distFromPlayer;
        private float explodeDist;

        private const float triggerDistance = 0.7f;

        private void Start()
        {
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

            explodeDist = Random.Range(0f, distFromPlayer * triggerDistance);
        }

        private void Update()
        {
            float dist = Vector2.Distance(gameObject.transform.position, PlayerController.singleton.gameObject.transform.position);

            float t = dist - explodeDist;
            if (t < 0.1f && t > -0.1f)
            {
                // explode
                Destroy(gameObject);
            }
        }
    }
}
