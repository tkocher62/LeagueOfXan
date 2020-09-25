﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject eventSystem;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(eventSystem);
    }
}
