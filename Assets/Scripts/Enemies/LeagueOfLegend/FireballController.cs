using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies.LeagueOfLegend
{
	class FireballController : MonoBehaviour
	{
        private void Start()
        {
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;

            Vector3 targ = PlayerController.singleton.gameObject.transform.position;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 180;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            rb.AddForce((PlayerController.singleton.transform.position - gameObject.transform.position).normalized * 500f);
        }
	}
}
