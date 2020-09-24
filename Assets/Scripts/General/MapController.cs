using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    internal static MapController singleton;

    public Tilemap tilemap;
    public Sprite openDoor;

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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDoorOpen)
        {
            // teleport to next stage
        }
    }

    private void OpenDoor()
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = openDoor;
        tilemap.SetTile(new Vector3Int(7, 4, 0), tile);
        isDoorOpen = true;
    }
}
