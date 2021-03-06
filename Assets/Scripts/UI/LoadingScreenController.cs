﻿using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    public List<GameObject> objToLoad;

    private const float loadTime = 1f;
    private float timer;

    private void Start()
    {
        timer = loadTime;
    }

    private void FixedUpdate()
    {
        timer = Mathf.Clamp(timer -= Time.fixedDeltaTime, 0f, loadTime);
        if (timer == 0f)
        {
            foreach (GameObject obj in objToLoad) obj.SetActive(true);
            //SceneManager.LoadScene(11);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            TimerController.ResetTimer();
            TimerController.StartTimer();
        }
    }
}
