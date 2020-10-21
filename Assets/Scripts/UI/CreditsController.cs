using Assets.Scripts.General;
using Assets.Scripts.UI;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public Text forXan;
    public Text title;
    public List<Text> children;
    public Button continueButton;

    private const float period = 1.5f;

    private void Start()
    {
        TimerController.StopTimer();
        long time = TimerController.GetTime();
        if (time < SaveManager.saveData.fastestTime)
        {
            SaveManager.saveData.fastestTime = time;
            SaveManager.SaveData();
        }

        AchievementManager.Achieve("beat_the_game");

        forXan.SetInvisible();
        foreach (Text t in forXan.GetComponentsInChildren<Text>())
        {
            t.SetInvisible();
        }
        foreach (Text t in title.GetComponentsInChildren<Text>())
        {
            t.SetInvisible();
        }
        foreach (Text text in children)
        {
            text.SetInvisible();
            foreach (Text t in text.GetComponentsInChildren<Text>())
            {
                t.SetInvisible();
            }
        }
        continueButton.image.SetInvisible();

        Timing.RunCoroutine(Fade().CancelWith(gameObject));
    }

    private IEnumerator<float> Fade()
    {
        Utils.FadeWithChildren(forXan, 1, period, false);
        yield return Timing.WaitForSeconds(period * 2);
        Utils.FadeWithChildren(forXan, 0, period, false);
        yield return Timing.WaitForSeconds(period);
        Utils.CrossFadeAlphaFixed(title, 1, period, false);
        Utils.FadeWithChildren(title, 1, period, false);
        yield return Timing.WaitForSeconds(period - 0.5f);
        foreach (Text text in children)
        {
            Utils.FadeWithChildren(text, 1, period, false);
        }
        yield return Timing.WaitForSeconds(period - 0.5f);
        Utils.CrossFadeAlphaFixed(continueButton.image, 1f, period, false);
    }

    public void Continue() => SceneManager.LoadScene(0);
}
