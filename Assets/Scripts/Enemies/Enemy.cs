using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public abstract class Enemy : Entity
	{
        public float health;

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
    }
}
