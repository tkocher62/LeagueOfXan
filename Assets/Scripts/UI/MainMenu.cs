﻿using Assets.Scripts.General;
using Assets.Scripts.UI;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        if (!File.Exists(SaveManager.path))
        {
            // Create save file
            SaveManager.InitData();
            SaveManager.SaveData();
        }
    }

    private void Start()
    {
        // Load save data
        SaveManager.LoadData();
        print(SaveManager.saveData.fastestTime);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
        AchievementManager.Achieve("play_the_game");
        Destroy(MusicController.instance.gameObject);
    }

    public void AchievementsMenu() => SceneManager.LoadScene(2);

    public void OptionsMenu() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
