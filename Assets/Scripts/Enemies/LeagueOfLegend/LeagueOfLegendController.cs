using Assets.Scripts.Enemies;
using Assets.Scripts.General;
using MEC;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeagueOfLegendController : Enemy
{
    // Singleton
    internal static LeagueOfLegendController singleton;

    // Publics
    public GameObject waypoints;
    public HealthBar healthBar;

    // Inits
    private Transform[] _waypoints;

    // Privates
    private GameObject prefab;

    private Vector3 currentWaypoint;

    private List<GameObject> enemyPrefabs;

    private SpriteRenderer render;

    // Internals
    internal Collider2D collide;

    // Timings
    private const float slamDistance = 3.6f;
    private const float movementIntervalStart = 4f;
    private const float movementIntervalEnd = 8f;

    // Values
    private const int enemySpawnAmount = 4;
    private int minimumAttacksBeforeSpawn = 5;

    // Damages
    private const float slamDamage = 40;

    private void Start()
    {
        // DELETE THIS WHEN DONE TESTING
        SaveManager.LoadData();

        singleton = this;

        prefab = Resources.Load<GameObject>("Prefabs/Projectiles/Fireball");

        Slider slider = healthBar.GetComponent<Slider>();
        slider.maxValue = health;
        slider.value = health;

        _waypoints = waypoints.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name != "Waypoints").ToArray();
        currentWaypoint = Vector3.zero;

        render = GetComponent<SpriteRenderer>();
        collide = GetComponent<Collider2D>();

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

        Attack();
    }

    private void Update()
    {
        if (currentWaypoint != null)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint, step);
        }

        if (Input.GetKeyDown(KeyCode.H)) Timing.RunCoroutine(Spawn().CancelWith(gameObject));
        else if (Input.GetKeyDown(KeyCode.J)) Instantiate(prefab, gameObject.transform.position, Quaternion.identity);
    }

    private IEnumerator<float> MovementTimer()
    {
        while (health > 0f)
        {
            yield return Timing.WaitForSeconds(Random.Range(movementIntervalStart, movementIntervalEnd));
            SetRandomWaypoint();
        }
    }

    private void SetRandomWaypoint()
    {
        // 1 in 5 chance to move at the player
        int a = Random.Range(0, 5);
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

    internal void Attack()
    {
        Debug.Log("CHOOSING BOSS ATTACK");
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
            //switch (Random.Range(0, 7))
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
            int a = Random.Range(min, 7);
            switch (a)
            {
                case 0:
                    Timing.RunCoroutine(Spawn().CancelWith(gameObject));
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    Fireball();
                    break;
                case 7:
                case 8:
                case 9:
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
        Debug.Log("BOSS ATTACK: SPAWN");
        Timing.RunCoroutine(FadeBoss(true).CancelWith(gameObject));

        MapController.singleton.enemies = enemySpawnAmount;

        for (int i = 0; i < enemySpawnAmount; i++)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Utils.Instantiate(enemy, new Vector2(Random.Range(-8, 8), Random.Range(-4, 4)), Quaternion.identity);
            yield return Timing.WaitForSeconds(0.3f);
        }
    }

    private IEnumerator<float> Slam()
    {
        // visually show prep now
        Debug.Log("BOSS ATTACK: SLAM");
        yield return Timing.WaitForSeconds(0.75f);

        if (Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position) < slamDistance)
        {
            PlayerController.singleton.Damage(slamDamage);
        }

        // show slam
        Timing.CallDelayed(1f, () => Attack());
    }

    private void Fireball()
    {
        Debug.Log("BOSS ATTACK: FIREBALL");
        Instantiate(prefab, transform.position, Quaternion.identity);
    }

    private void Laser()
    {
        Debug.Log("BOSS ATTACK: LASER");
        Timing.CallDelayed(1f, () => Attack());
    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
        healthBar.SetHealthBar(base.health);
    }

    protected override void Kill()
    {
        // ending stuff
    }
}
