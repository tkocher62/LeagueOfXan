using MEC;
using Pathfinding;
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

		protected void Move(Rigidbody2D rb, Vector2 direction, float playerWidth, float playerHeight, float movementSpeed)
		{
			Vector3 viewPos = transform.position;
			viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
			viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
			rb.MovePosition((Vector2)viewPos + (direction * movementSpeed * Time.deltaTime));
		}

		private IEnumerator<float> FlashRedCoroutine(SpriteRenderer render)
		{
			if (render.color == Color.white)
			{
				render.color = Color.red;
				yield return Timing.WaitForSeconds(0.2f);
				if (render != null)
				{
					render.color = Color.white;
				}
			}
		}
	}
}
