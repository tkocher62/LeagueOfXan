using Assets.Scripts.General;
using Assets.Scripts.UI;
using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer vid;
    public Button button;

    private Text btnText;

    private const float fadeDelay = 0.5f;
    private const float period = 2f;

    private void Start()
    {
        vid.loopPointReached += CheckOver;
        vid.SetDirectAudioVolume(0, SaveManager.saveData.musicVolume);

        if (button != null)
        {
            btnText = button.GetComponentInChildren<Text>();

            button.image.SetInvisible();
            btnText.SetInvisible();
        }

        foreach (Button button in GetComponentsInChildren<Button>(true))
        {
            button.onClick.AddListener(delegate { SfxController.singleton.PlayButtonClick(); });
        }

        Timing.RunCoroutine(Fade().CancelWith(gameObject));
    }

    private IEnumerator<float> Fade()
    {
        yield return Timing.WaitForSeconds(fadeDelay);
        Assets.Scripts.UI.Utils.CrossFadeAlphaFixed(button.image, 1, period, false);
        Assets.Scripts.UI.Utils.CrossFadeAlphaFixed(btnText, 1, period, false);
    }

    private void CheckOver(VideoPlayer vp)
    {
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
