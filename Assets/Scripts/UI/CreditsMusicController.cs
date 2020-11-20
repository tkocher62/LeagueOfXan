using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.UI
{
	class CreditsMusicController : MonoBehaviour
	{
        private AudioSource source;

        private float currentTime;
        private float start;

        private const float duration = 2.5f;

        private void Start()
        {
            currentTime = 0f;
            source = GetComponent<AudioSource>();
            start = source.volume;
        }

        private void Update()
        {
            if (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                source.volume = Mathf.Lerp(start, SaveManager.saveData.musicVolume, currentTime / duration);
            }
        }
    }
}
