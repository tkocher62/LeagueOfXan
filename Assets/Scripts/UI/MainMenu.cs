using Assets.Scripts.General;
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
        SaveManager.LoadData();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(3);
        AchievementManager.Achieve("play_the_game");
        Destroy(MenuMusicController.instance.gameObject);
    }

    public void AchievementsMenu() => SceneManager.LoadScene(2);

    public void OptionsMenu() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
