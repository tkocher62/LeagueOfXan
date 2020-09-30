using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	public class Entity : MonoBehaviour
	{
		public void FlashRed()
		{
			Timing.RunCoroutine(FlashRedCoroutine(GetComponent<SpriteRenderer>()).CancelWith(gameObject));
		}

		private IEnumerator<float> FlashRedCoroutine(SpriteRenderer render)
		{
			Color defaultColor = render.color;
			render.color = new Color(255, 0, 0);
			yield return Timing.WaitForSeconds(0.2f);
			render.color = defaultColor;
		}
	}
}
