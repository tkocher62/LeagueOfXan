using Assets.Scripts.General;
using Assets.Scripts.UI;
using Pathfinding;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private GameObject potion;

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

            potion = Resources.Load<GameObject>("Prefabs/Items/HealthPotion");

            isInit = true;
        }

        protected void Move(Vector2 movement)
        {
            Move(rb, movement, playerWidth, playerHeight, movementSpeed);
        }

        protected virtual void Kill()
        {
            MapController.singleton.enemies--;

            if (!SaveManager.saveData.isEasyMode)
            {
                SaveManager.saveData.enemyKillCount++;
                if (SaveManager.saveData.enemyKillCount == 100)
                {
                    AchievementManager.Achieve("kill_100_enemies");
                }
            }

            // Boss stage
            if (SceneManager.GetActiveScene().buildIndex == 12)
            {
                General.Utils.Instantiate(potion, gameObject.transform.position, Quaternion.identity);
            }
        }

        public virtual void Damage(float damage)
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
