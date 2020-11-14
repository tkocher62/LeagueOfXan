using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    internal static MenuMusicController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.GetComponent<AudioSource>().volume = SaveManager.saveData.musicVolume;
    }
}
