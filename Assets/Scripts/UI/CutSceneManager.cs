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

        btnText = button.GetComponentInChildren<Text>();

        button.image.SetInvisible();
        btnText.SetInvisible();

        Timing.RunCoroutine(Fade().CancelWith(gameObject));
    }

    private IEnumerator<float> Fade()
    {
        yield return Timing.WaitForSeconds(fadeDelay);
        Utils.CrossFadeAlphaFixed(button.image, 1, period, false);
        Utils.CrossFadeAlphaFixed(btnText, 1, period, false);
    }

    private void CheckOver(VideoPlayer vp)
    {
        LoadGame();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(4);
    }
}
