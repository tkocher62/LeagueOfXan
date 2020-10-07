using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
