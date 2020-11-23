using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    internal static SfxController singleton;

    public AudioClip buttonClick;

    private AudioSource source;

    private void Start()
    {
        singleton = this;

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        ChangeVolume(SaveManager.saveData.sfxVolume);
    }

    internal void ChangeVolume(float volume) => source.volume = volume;

    public void ButtonClickSound()
    {
        source.clip = buttonClick;
        source.pitch = 0.8f;
        source.Play();
        source.pitch = 1f;
    }
}
