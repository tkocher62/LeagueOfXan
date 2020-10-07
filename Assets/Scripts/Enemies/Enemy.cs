using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public abstract class Enemy : Entity
	{
        protected void Move(Rigidbody2D rb, Vector2 direction, float playerWidth, float playerHeight, float movementSpeed)
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
            rb.MovePosition((Vector2)viewPos + (direction * movementSpeed * Time.deltaTime));
        }

        public abstract void Damage(float damage);

        protected void Kill() => MapController.singleton.enemies--;
	}
}
