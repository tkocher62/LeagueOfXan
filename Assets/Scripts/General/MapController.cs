using MEC;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    internal static MapController singleton;

    public Sprite openDoor;
    public List<Sprite> closedDoors;
    public Tilemap targetTileMap;

    private Tile openDoorTile;

    private bool isDoorOpen;
    private int _enemies;

    internal int enemies {
        get
        {
            return _enemies;
        }
        set
        {
            _enemies = value;

            if (_enemies == 0)
            {
                // If on boss stage
                if (SceneManager.GetActiveScene().buildIndex == 12)
                {
                    Timing.RunCoroutine(LeagueOfLegendController.singleton.FadeBoss(false).CancelWith(LeagueOfLegendController.singleton.gameObject));
                }
                else
                {
                    OpenDoor();
                }
            }
        }
    }

    private void Start()
    {
        singleton = this;

        isDoorOpen = false;

        enemies = GameObject.FindGameObjectsWithTag("Enemy").Count();

        openDoorTile = ScriptableObject.CreateInstance<Tile>();
        openDoorTile.sprite = openDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDoorOpen)
        {
            // Load next scene
            int id = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(id);
            if (id != SceneManager.sceneCountInBuildSettings - 1)
            {
                PlayerController.singleton.Spawn();
            }
            else
            {
                Destroy(PlayerController.singleton.gameObject);
            }
        }
    }

    private void OpenDoor()
    {
        foreach (Vector3Int pos in targetTileMap.cellBounds.allPositionsWithin)
        {
            if (closedDoors.Contains(targetTileMap.GetSprite(pos)))
            {
                targetTileMap.SetTile(pos, openDoorTile);
            }
        }

        isDoorOpen = true;
    }
}
