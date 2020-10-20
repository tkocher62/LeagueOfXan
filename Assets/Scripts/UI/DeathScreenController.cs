using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public void Retry()
    {
        DestroyAllDontDestroyOnLoad();

        SceneManager.LoadScene(4);
    }

    public void MainMenu()
    {
        DestroyAllDontDestroyOnLoad();

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
