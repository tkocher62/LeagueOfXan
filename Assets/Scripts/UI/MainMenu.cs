using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() => SceneManager.LoadScene(3);

    public void AchievementsMenu() => SceneManager.LoadScene(2);

    public void OptionsMenu() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
