using Assets.Scripts.Enemies;
using Assets.Scripts.General;
using MEC;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeagueOfLegendController : Enemy
{
    // Singleton
    internal static LeagueOfLegendController singleton;

    // Prefabs
    public GameObject waypoints;
    public HealthBar healthBar;

    // Inits
    private Transform[] _waypoints;

    // Privates
    private GameObject fireball;
    private GameObject laser;
    private GameObject potion;
    private GameObject explosionPrefab;
    private GameObject body_1;
    private GameObject body_2;

    private Vector3 currentWaypoint;

    private List<GameObject> enemyPrefabs;

    private List<GameObject> potions;

    // Internals
    internal Collider2D collide;

    internal SpriteRenderer render;

    internal List<GameObject> projectiles;

    internal bool isUsingLaser = false;

    // Timings
    private const float slamDistance = 3.6f;
    private const float movementIntervalStart = 3f;
    private const float movementIntervalEnd = 7f;

    // Values
    private const int enemySpawnAmount = 4;
    private int attacksSinceLastSpawn = 0;
    private int minimumAttacksBeforeSpawn = 5;
    private const int maxAttacksBeforeSpawn = 10;
    private float startingHealth = 150f;
    private float healthLostSinceLastMove = 0f;
    private Vector3 lastPos;

    // Damages
    private const float slamDamage = 40;

    // Frames
    public Sprite s_Neutral;
    public Sprite s_Fireball;
    public Sprite s_SlamCharge;
    public Sprite s_Slam;
    public Sprite s_Laser;

    private void Start()
    {
        // todo: DELETE THIS WHEN DONE TESTING
        SaveManager.LoadData();
        // --

        singleton = this;

        fireball = Resources.Load<GameObject>("Prefabs/Projectiles/Fireball");
        laser = Resources.Load<GameObject>("Prefabs/Effects/Laser");
        potion = Resources.Load<GameObject>("Prefabs/Items/HealthPotion");
        explosionPrefab = Resources.Load<GameObject>("Prefabs/Effects/Explosion_LoL");
        body_1 = Resources.Load<GameObject>("Prefabs/Decorations/body_1");
        body_2 = Resources.Load<GameObject>("Prefabs/Decorations/body_2");

        Slider slider = healthBar.GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;

        _waypoints = waypoints.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name != "Waypoints").ToArray();
        currentWaypoint = gameObject.transform.position;
        lastPos = gameObject.transform.position;

        render = GetComponent<SpriteRenderer>();
        collide = GetComponent<Collider2D>();

        potions = new List<GameObject>();

        projectiles = new List<GameObject>();

        enemyPrefabs = new List<GameObject>()
        {
            Resources.Load<GameObject>("Prefabs/Enemies/Chort"),
            Resources.Load<GameObject>("Prefabs/Enemies/Demon"),
            Resources.Load<GameObject>("Prefabs/Enemies/Necromancer"),
            Resources.Load<GameObject>("Prefabs/Enemies/Skeleton"),
            Resources.Load<GameObject>("Prefabs/Enemies/Swampy"),
            Resources.Load<GameObject>("Prefabs/Enemies/Zombie")
        };

        Timing.RunCoroutine(MovementTimer().CancelWith(gameObject));

        startingHealth = health;

        HealthPotionController.amount = 6f;

        render.sprite = s_Neutral;

        Attack();
    }

    private void Update()
    {
        if (currentWaypoint != null)
        {
            lastPos = transform.position;
            float step = movementSpeed * Time.deltaTime * (health > 0f ? 1f : 0f);
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, step);
        }

        if (Input.GetKeyDown(KeyCode.H)) Timing.RunCoroutine(Spawn().CancelWith(gameObject));
        else if (Input.GetKeyDown(KeyCode.J)) Instantiate(fireball, gameObject.transform.position, Quaternion.identity);
        else if (Input.GetKeyDown(KeyCode.K)) Instantiate(laser, gameObject.transform.position, Quaternion.identity);
    }

    private IEnumerator<float> MovementTimer()
    {
        while (health > 0f)
        {
            yield return Timing.WaitForSeconds(Random.Range(movementIntervalStart, movementIntervalEnd));
            if (!isUsingLaser) SetRandomWaypoint();
        }
    }

    private void SetRandomWaypoint()
    {
        // 1 in 4 chance to move at the player
        int a = Random.Range(0, 4);
        if (a != 0)
        {
            int i = Random.Range(0, _waypoints.Length);
            if (_waypoints[i].position == currentWaypoint)
            {
                i++;
                if (i == _waypoints.Length)
                {
                    i = 0;
                }
            }
            currentWaypoint = _waypoints[i].position;
        }
        else
        {
            currentWaypoint = PlayerController.singleton.transform.position;
        }
    }

    internal void SpawnEnemyPotion(Vector3 pos)
    {
        potions.Add(Utils.Instantiate(potion, pos, Quaternion.identity));
    }

    internal void Attack()
    {
        ChooseAttack();
    }

    private void ChooseAttack()
    {
        if (Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position) < slamDistance)
        {
            Timing.RunCoroutine(Slam().CancelWith(gameObject));
        }
        else
        {

            int min = 1;
            if (minimumAttacksBeforeSpawn == 0)
            {
                minimumAttacksBeforeSpawn = 5;
                min = 0;
            }
            else
            {
                minimumAttacksBeforeSpawn--;
            }

            int val = Random.Range(min, 9);

            if (val != 0) attacksSinceLastSpawn++;
            if (attacksSinceLastSpawn == maxAttacksBeforeSpawn)
            {
                attacksSinceLastSpawn = 0;
                val = 0;
            }

            switch (val)
            {
                case 0:
                    Timing.RunCoroutine(Spawn().CancelWith(gameObject));
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    Fireball();
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    Laser();
                    break;
            }
        }
    }

    internal IEnumerator<float> FadeBoss(bool faded)
    {
        yield return Timing.WaitUntilDone(Timing.RunCoroutine(Fade(render, 0.05f, faded).CancelWith(gameObject)));
        collide.enabled = !faded;
        if (!faded)
        {
            yield return Timing.WaitForSeconds(2f);
            Attack();
        }
    }

    private IEnumerator<float> Fade(SpriteRenderer render, float speed, bool fadeOut)
    {
        Color c = render.color;
        if (fadeOut)
        {
            while (c.a > 0f)
            {
                c.a = Mathf.Clamp(c.a - 0.1f, 0f, 1f);
                render.color = c;
                yield return Timing.WaitForSeconds(speed);
            }
        }
        else
        {
            render.color = new Color(c.r, c.g, c.b, 0f);
            while (c.a < 1f)
            {
                c.a = Mathf.Clamp(c.a + 0.1f, 0f, 1f);
                render.color = c;
                yield return Timing.WaitForSeconds(speed);
            }
        }
    }

    private IEnumerator<float> Spawn()
    {
        Timing.RunCoroutine(FadeBoss(true).CancelWith(gameObject));

        MapController.singleton.enemies = enemySpawnAmount;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Utils.Instantiate(enemy, new Vector2(Random.Range(-8, 8), Random.Range(-4, 4)), Quaternion.identity);
            yield return Timing.WaitForSeconds(0.3f);
        }
    }

    internal void FinishSpawn()
    {
        Timing.RunCoroutine(FadeBoss(false).CancelWith(gameObject));
        foreach (GameObject potion in potions) Destroy(potion);
        potions.Clear();
    }

    private IEnumerator<float> Slam()
    {
        render.sprite = s_SlamCharge;
        yield return Timing.WaitForSeconds(0.75f);

        if (Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position) < slamDistance && health > 0f)
        {
            PlayerController.singleton.Damage(slamDamage);
        }

        render.sprite = s_Slam;

        Timing.RunCoroutine(Utils.CallDelayed(0.5f, () => Attack()).CancelWith(gameObject));
    }

    private void Fireball()
    {
        render.sprite = s_Fireball;
        projectiles.Add(Instantiate(fireball, transform.position, Quaternion.identity));
    }

    private void Laser()
    {
        if (lastPos == transform.position)
        {
            render.sprite = s_Laser;
            isUsingLaser = true;
            Vector3 pos = transform.position;
            // Set coordinates to move laser to the boss's head
            pos.x += 0.241f;
            pos.y += 1.345f;
            projectiles.Add(Instantiate(laser, pos, Quaternion.identity));
        }
        Timing.RunCoroutine(Utils.CallDelayed(1.3f, () => Attack()).CancelWith(gameObject));
    }

    public override void Damage(float damage)
    {
        if (render.color.a == 0f) return;

        health -= damage;
        if (health <= 0f)
        {
            Kill();
            GameObject explosion = Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            explosion.transform.localScale *= 6f;
        }
        else FlashRed();

        healthLostSinceLastMove += damage;
        if (healthLostSinceLastMove > startingHealth * 0.2f)
        {
            SetRandomWaypoint();
            healthLostSinceLastMove = 0f;
        }
        healthBar.SetHealthBar(base.health);
    }

    internal void BreakBoss()
    {
        if (health > 0f)
        {
            Vector3 body1Pos = gameObject.transform.position;
            body1Pos.x -= 0.001f;
            body1Pos.y -= 0.436f;
            Instantiate(body_1, body1Pos, Quaternion.Euler(0, 0, -90.173f));

            Vector3 body2Pos = gameObject.transform.position;
            body2Pos.x -= 1.191f;
            body2Pos.y -= 0.396f;
            Instantiate(body_2, body2Pos, Quaternion.Euler(0, 0, 29.773f));

            Destroy(gameObject);
        }
    }

    protected override void Kill()
    {
        MapController.singleton.enemies = 0;
        foreach (GameObject obj in projectiles)
        {
            Destroy(obj);
        }
        SfxController.singleton.PlayBigExplosion();
        projectiles.Clear();
    }
}
