using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
	internal static class Utils
	{
        internal static void FadeWithChildren(Graphic img, float alpha, float duration, bool ignoreTimeScale)
        {
            CrossFadeAlphaFixed(img, alpha, duration, ignoreTimeScale);
            foreach (Text t in img.GetComponentsInChildren<Text>())
            {
                CrossFadeAlphaFixed(t, alpha, duration, ignoreTimeScale);
            }
        }

        internal static void CrossFadeAlphaFixed(Graphic img, float alpha, float duration, bool ignoreTimeScale)
        {
            Color fixedColor = img.color;
            fixedColor.a = 1;
            img.color = fixedColor;

            img.CrossFadeAlpha(alpha == 1 ? 0f : 1f, 0f, true);

            img.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
        }

        internal static void SetInvisible(this Graphic text)
        {
            Color c = text.color;
            c.a = 0f;
            text.color = c;
        }
    }
}
