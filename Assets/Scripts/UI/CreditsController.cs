using MEC;
using System.Collections;
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
        SetInvisible(forXan);
        foreach (Text t in forXan.GetComponentsInChildren<Text>())
        {
            SetInvisible(t);
        }
        foreach (Text t in title.GetComponentsInChildren<Text>())
        {
            SetInvisible(t);
        }
        foreach (Text text in children)
        {
            SetInvisible(text);
            foreach (Text t in text.GetComponentsInChildren<Text>())
            {
                SetInvisible(t);
            }
        }
        SetInvisible(continueButton.image);

        Timing.RunCoroutine(Fade().CancelWith(gameObject));
    }

    private void SetInvisible(Graphic text)
    {
        Color c = text.color;
        c.a = 0f;
        text.color = c;
    }

    private IEnumerator<float> Fade()
    {
        FadeWithChildren(forXan, 1, period, false);
        yield return Timing.WaitForSeconds(period * 2);
        FadeWithChildren(forXan, 0, period, false);
        yield return Timing.WaitForSeconds(period);
        CrossFadeAlphaFixed(title, 1, period, false);
        FadeWithChildren(title, 1, period, false);
        yield return Timing.WaitForSeconds(period - 0.5f);
        foreach (Text text in children)
        {
            FadeWithChildren(text, 1, period, false);
        }
        yield return Timing.WaitForSeconds(period - 0.5f);
        CrossFadeAlphaFixed(continueButton.image, 1f, period, false);
    }

    private void FadeWithChildren(Graphic img, float alpha, float duration, bool ignoreTimeScale)
    {
        CrossFadeAlphaFixed(img, alpha, period, false);
        foreach (Text t in img.GetComponentsInChildren<Text>())
        {
            CrossFadeAlphaFixed(t, alpha, period, false);
        }
    }

    private void CrossFadeAlphaFixed(Graphic img, float alpha, float duration, bool ignoreTimeScale)
    {
        Color fixedColor = img.color;
        fixedColor.a = 1;
        img.color = fixedColor;

        img.CrossFadeAlpha(alpha == 1 ? 0f : 1f, 0f, true);

        img.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }

    public void Continue() => SceneManager.LoadScene(0);
}
