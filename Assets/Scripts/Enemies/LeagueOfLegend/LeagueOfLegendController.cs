using Assets.Scripts.Enemies;
using MEC;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LeagueOfLegendController : Enemy
{
    // Publics
    public GameObject waypoints;

    // Inits
    private Transform[] _waypoints;

    // Privates
    private bool isAttackInProgress;

    private Transform currentWaypoint;

    private List<GameObject> enemyPrefabs;

    // Timings
    private const float slamDistance = 4.3f;
    private const float movementInterval = 5f;

    // Values
    private const int enemySpawnAmount = 3;

    // Damages
    private const float slamDamage = 40;

    private void Start()
    {
        _waypoints = waypoints.GetComponentsInChildren<Transform>().Where(t => t.gameObject.name != "Waypoints").ToArray();
        currentWaypoint = null;

        enemyPrefabs = new List<GameObject>()
        {
            Resources.Load<GameObject>("Prefabs/Enemies/Chort"),
            Resources.Load<GameObject>("Prefabs/Enemies/Demon"),
            Resources.Load<GameObject>("Prefabs/Enemies/Necromancer"),
            Resources.Load<GameObject>("Prefabs/Enemies/Skeleton"),
            Resources.Load<GameObject>("Prefabs/Enemies/Swampy"),
            Resources.Load<GameObject>("Prefabs/Enemies/Zombie")
        };


        isAttackInProgress = false;

        Timing.RunCoroutine(MovementTimer().CancelWith(gameObject));
    }

    private void Update()
    {
        if (currentWaypoint != null)
        {
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, step);
        }

        if (Input.GetKeyDown(KeyCode.H)) Timing.RunCoroutine(Spawn().CancelWith(gameObject));
    }

    private void FixedUpdate()
    {
        /*if (!isAttackInProgress)
        {
            Attack();
        }*/
    }

    private IEnumerator<float> MovementTimer()
    {
        while (health > 0f)
        {
            yield return Timing.WaitForSeconds(movementInterval);
            //SetRandomWaypoint();
        }
    }

    private void SetRandomWaypoint()
    {
        int i = Random.Range(0, _waypoints.Length);
        if (_waypoints[i] == currentWaypoint)
        {
            i++;
            if (i == _waypoints.Length)
            {
                i = 0;
            }
        }
        currentWaypoint = _waypoints[i];
    }

    private void Attack()
    {
        Debug.Log("choosing attack");
        ChooseAttack();
        isAttackInProgress = true;
    }

    private void ChooseAttack()
    {
        if (Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position) < slamDistance)
        {
            Timing.RunCoroutine(Slam().CancelWith(gameObject));
        }
        else
        {
            switch (Random.Range(0, 3))
            {
                case 0:
                    Spawn();
                    break;
                case 1:
                    Fireball();
                    break;
                case 2:
                    Laser();
                    break;
            }
        }
    }

    private IEnumerator<float> Spawn()
    {
        // Spawn white orb or some sort of marker just before each enemy spawns
        for (int i = 0; i < enemySpawnAmount; i++)
        {
            Vector2 scwp = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

            Vector2 spawnPosition = new Vector2(
                Random.Range(scwp.y + 5, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height - 5)).y), 
                Random.Range(scwp.x + 5, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - 5, 0)).x)
            );

            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Instantiate(enemy, spawnPosition, Quaternion.identity);

            // Maybe remove delay, whicveer looks better with the spawn orb effect
            yield return Timing.WaitForSeconds(0.3f);
        }
    }

    private IEnumerator<float> Slam()
    {
        // visually show prep now
        Debug.Log("prep");
        yield return Timing.WaitForSeconds(0.75f);

        switch (Random.Range(0, 3))
        {
            case 0:
                Spawn();
                break;
            case 1:
                Fireball();
                break;
            case 2:
                Laser();
                break;
        }

        if (Vector3.Distance(transform.position, PlayerController.singleton.gameObject.transform.position) < slamDistance)
        {
            //PlayerController.singleton.Damage(slamDamage);
            Debug.Log("hit");
        }
        else Debug.Log("miss");
    }

    private void Fireball()
    {
        // explode when it hits last logged player position
    }

    private void Laser()
    {

    }
}
