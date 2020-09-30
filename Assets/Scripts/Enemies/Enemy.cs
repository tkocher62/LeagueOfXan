using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
	public abstract class Enemy : Entity
	{
		public abstract void Damage(float damage);

		public void Kill() => MapController.singleton.enemies--;
	}
}
