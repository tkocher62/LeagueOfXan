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
        if (!SaveManager.saveData.isEasyMode)
        {
            if (time < SaveManager.saveData.fastestTime || SaveManager.saveData.fastestTime == -1)
            {
                SaveManager.saveData.fastestTime = time;
                SaveManager.SaveData();
            }

            AchievementManager.Achieve("beat_the_game");
        }

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
        continueButton.onClick.AddListener(delegate { SfxController.singleton.PlayButtonClick(); });

        Timing.RunCoroutine(Fade().CancelWith(gameObject));
    }

    private IEnumerator<float> Fade()
    {
        Assets.Scripts.UI.Utils.FadeWithChildren(forXan, 1, period, false);
        yield return Timing.WaitForSeconds(period * 2);
        Assets.Scripts.UI.Utils.FadeWithChildren(forXan, 0, period, false);
        yield return Timing.WaitForSeconds(period);
        Assets.Scripts.UI.Utils.CrossFadeAlphaFixed(title, 1, period, false);
        Assets.Scripts.UI.Utils.FadeWithChildren(title, 1, period, false);
        yield return Timing.WaitForSeconds(period - 0.5f);
        foreach (Text text in children)
        {
            Assets.Scripts.UI.Utils.FadeWithChildren(text, 1, period, false);
        }
        yield return Timing.WaitForSeconds(period - 0.5f);
        Assets.Scripts.UI.Utils.CrossFadeAlphaFixed(continueButton.image, 1f, period, false);
    }

    public void Continue() => SceneManager.LoadScene(0);
}
