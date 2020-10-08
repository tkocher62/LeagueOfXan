using MEC;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public abstract class Enemy : Entity
	{
        public float health;
        public float movementSpeed;

        protected Vector2 movement;
        protected float playerWidth, playerHeight;

        private SpriteRenderer render;
        private Rigidbody2D rb;
        private AIDestinationSetter ai;
        private bool isInit = false;

        protected void Init(SpriteRenderer render, Rigidbody2D rb, AIDestinationSetter ai = null)
        {
            this.render = render;
            this.rb = rb;
            this.ai = ai;

            playerWidth = render.bounds.extents.x;
            playerHeight = render.bounds.extents.y;

            if (this.ai != null)
            {
                this.ai.target = PlayerController.singleton.transform;
            }

            isInit = true;
        }

        protected void Move(Vector2 movement)
        {
            Move(rb, movement, playerWidth, playerHeight, movementSpeed);
        }

        protected void Kill() => MapController.singleton.enemies--;

        public void Damage(float damage)
        {
            health -= damage;
            FlashRed();
            if (health <= 0f)
            {
                Destroy(gameObject);
                Kill();
            }
        }

        private void Update()
        {
            if (isInit)
            {
                movement = (PlayerController.singleton.gameObject.transform.position - transform.position).normalized;

                if (movement != Vector2.zero)
                {
                    render.flipX = movement.x < 0;
                }
            }
        }
    }
}
