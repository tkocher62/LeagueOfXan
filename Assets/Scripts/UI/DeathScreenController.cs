using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void RetryStage()
    {
        DestroyAllDontDestroyOnLoad();

        LoadingScreenInformation.sceneId = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        DestroyAllDontDestroyOnLoad();

        LoadingScreenInformation.sceneId = -1;
        SceneManager.LoadScene(0);
    }

    private void DestroyAllDontDestroyOnLoad()
    {
        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
        {
            Destroy(root);
        }
    }
}
