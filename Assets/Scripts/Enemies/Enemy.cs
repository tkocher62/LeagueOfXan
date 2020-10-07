using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public abstract class Enemy : Entity
	{
        public float health;

        protected void Move(Rigidbody2D rb, Vector2 direction, float playerWidth, float playerHeight, float movementSpeed)
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, ScreenBorderController.screenBounds.x * -1 + playerWidth, ScreenBorderController.screenBounds.x - playerWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, ScreenBorderController.screenBounds.y * -1 + playerHeight, ScreenBorderController.screenBounds.y - playerHeight);
            rb.MovePosition((Vector2)viewPos + (direction * movementSpeed * Time.deltaTime));
        }

        protected void Kill() => MapController.singleton.enemies--;

        public void Damage(float damage)
        {
            Debug.Log("ouch");
            health -= damage;
            FlashRed();
            if (health <= 0f)
            {
                Destroy(gameObject);
                Kill();
            }
        }
    }
}
