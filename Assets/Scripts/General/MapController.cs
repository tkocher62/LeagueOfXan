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
    public Tilemap wallTileMap;

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
                OpenDoor();
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
            // Save data
            DataController.character = PlayerController.singleton.curCharacter;
            DataController.health = PlayerController.singleton.health;

            // Load next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerController.singleton.Spawn();
        }
    }

    private void OpenDoor()
    {
        foreach (Vector3Int pos in wallTileMap.cellBounds.allPositionsWithin)
        {
            if (closedDoors.Contains(wallTileMap.GetSprite(pos)))
            {
                wallTileMap.SetTile(pos, openDoorTile);
            }
        }

        isDoorOpen = true;
    }
}
