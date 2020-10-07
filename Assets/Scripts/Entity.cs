using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class Entity : MonoBehaviour
	{
		protected void FlashRed()
		{
			Timing.RunCoroutine(FlashRedCoroutine(GetComponent<SpriteRenderer>()).CancelWith(gameObject));
		}

		private IEnumerator<float> FlashRedCoroutine(SpriteRenderer render)
		{
			if (render.color == Color.white)
			{
				render.color = Color.red;
				yield return Timing.WaitForSeconds(0.2f);
				render.color = Color.white;
			}
		}
	}
}
