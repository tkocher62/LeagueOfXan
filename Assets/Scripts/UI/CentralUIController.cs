using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CentralUIController : MonoBehaviour
{
    internal static CentralUIController singleton;

    public GameObject eventSystem;
    public GameObject deathScreen;
    public GameObject attackButton;

    private SfxController sfx;

    private void Start()
    {
        singleton = this;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(eventSystem);

        sfx = FindObjectOfType<SfxController>();
        foreach (Button button in GetComponentsInChildren<Button>(true).Where(x => x.gameObject != attackButton))
        {
            button.onClick.AddListener(delegate { sfx.ButtonClickSound(); });
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.sceneCountInBuildSettings - 1)
        {
            Destroy(gameObject);
            Destroy(eventSystem);
        }

        if (level == 12)
        {
            PlayerController.singleton.transform.localScale *= Utils.scale;
        }
    }
}
