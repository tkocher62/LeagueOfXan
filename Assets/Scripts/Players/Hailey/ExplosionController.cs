using UnityEngine;

namespace Assets.Scripts.Players.Hailey
{
	class ExplosionController : MonoBehaviour
	{
		private void Start() => Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
	}
}
