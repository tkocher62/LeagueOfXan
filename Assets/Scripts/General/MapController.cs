using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    internal static MapController singleton;

    public Sprite openDoor;
    public List<Sprite> closedDoors;
    public Tilemap wallTileMap;
    //public List<Tilemap> groundTileMaps;
    //public List<Tilemap> wallTileMaps;

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

        /*stage = 0;

        _groundTileMaps = new Dictionary<int, Tilemap>();
        _wallTileMaps = new Dictionary<int, Tilemap>();

        for (int i = 0; i < groundTileMaps.Count; i++)
        {
            _groundTileMaps.Add(i, groundTileMaps[i]);
        }

        for (int i = 0; i < wallTileMaps.Count; i++)
        {
            _wallTileMaps.Add(i, wallTileMaps[i]);
        }*/

        openDoorTile = ScriptableObject.CreateInstance<Tile>();
        openDoorTile.sprite = openDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isDoorOpen)
        {
            LoadNextStage();
        }
    }

    private void OpenDoor()
    {
        /*foreach (Vector3Int pos in _wallTileMaps[stage].cellBounds.allPositionsWithin)
        {
            if (closedDoors.Contains(_wallTileMaps[stage].GetSprite(pos)))
            {
                _wallTileMaps[stage].SetTile(pos, openDoorTile);
            }
        }*/

        foreach (Vector3Int pos in wallTileMap.cellBounds.allPositionsWithin)
        {
            if (closedDoors.Contains(wallTileMap.GetSprite(pos)))
            {
                wallTileMap.SetTile(pos, openDoorTile);
            }
        }

        isDoorOpen = true;
    }

    private void LoadNextStage()
    {
        /* groundTileMaps[stage].GetComponent<Toggle>().isOn = false;
         wallTileMaps[stage].GetComponent<Toggle>().isOn = false;

         stage++;

         groundTileMaps[stage].GetComponent<Toggle>().isOn = true;
         wallTileMaps[stage].GetComponent<Toggle>().isOn = true;

         PlayerController.singleton.Spawn();


         // load new enemies
         */
        Debug.Log("loading next scene");
    }
}
