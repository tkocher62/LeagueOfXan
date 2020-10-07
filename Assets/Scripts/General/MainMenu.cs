using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject musicController;

    private void Start()
    {
        DontDestroyOnLoad(musicController);
    }

    public void PlayGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    public void OptionsMenu() => SceneManager.LoadScene(1);

    public void QuitGame() => Application.Quit();
}
