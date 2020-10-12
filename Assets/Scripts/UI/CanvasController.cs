using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    internal static CanvasController singleton;

    public GameObject eventSystem;
    public GameObject deathScreen;

    private void Start()
    {
        singleton = this;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(eventSystem);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.sceneCountInBuildSettings - 1)
        {
            Destroy(gameObject);
            Destroy(eventSystem);
        }
    }
}
