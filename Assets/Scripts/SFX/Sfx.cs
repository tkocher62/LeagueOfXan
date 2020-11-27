using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    public AudioClip clip;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        ChangeVolume(SaveManager.saveData.sfxVolume);
    }

    internal void ChangeVolume(float volume) => source.volume = volume;

    public void Play()
    {
        source.clip = clip;
        source.Play();
    }
}
