using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    internal static MapController singleton;

    public Sprite openDoor;
    public List<Sprite> closedDoors;
    public List<Tilemap> groundTileMaps;
    public List<Tilemap> wallTileMaps;

    private Dictionary<int, Tilemap> _groundTileMaps;
    private Dictionary<int, Tilemap> _wallTileMaps;

    private Tile openDoorTile;

    private bool isDoorOpen;
    private int _enemies;
    private int stage;

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

        stage = 1;

        _groundTileMaps = new Dictionary<int, Tilemap>();
        _wallTileMaps = new Dictionary<int, Tilemap>();

        for (int i = 1; i <= groundTileMaps.Count; i++)
        {
            _groundTileMaps.Add(i, groundTileMaps[i - 1]);
        }

        for (int i = 1; i <= wallTileMaps.Count; i++)
        {
            _wallTileMaps.Add(i, wallTileMaps[i - 1]);
        }

        openDoorTile = ScriptableObject.CreateInstance<Tile>();
        openDoorTile.sprite = openDoor;
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
        foreach (Vector3Int pos in _wallTileMaps[stage].cellBounds.allPositionsWithin)
        {
            if (closedDoors.Contains(_wallTileMaps[stage].GetSprite(pos)))
            {
                _wallTileMaps[stage].SetTile(pos, openDoorTile);
            }
        }
        isDoorOpen = true;
    }

    private void LoadNextStage()
    {
        stage++;
        // Set player back to the start
        PlayerController.singleton.Spawn();
        // load new tilemaps
        // load new enemies
    }
}
