using UnityEngine;

namespace Assets.Scripts.Players.Hailey
{
	class ExplosionController : MonoBehaviour
	{
		private void Start() => Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

		public void BreakBoss()
		{
			if (LeagueOfLegendController.singleton?.health <= 0f)
			{
				LeagueOfLegendController.singleton.BreakBoss();
			}
		}
	}
}
