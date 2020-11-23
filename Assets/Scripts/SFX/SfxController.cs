using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    public AudioClip buttonClick;

    private AudioSource source;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        ChangeVolume(SaveManager.saveData.sfxVolume);
    }

    internal void ChangeVolume(float volume) => source.volume = volume;

    public void ButtonClickSound()
    {
        source.clip = buttonClick;
        source.Play();
    }
}
