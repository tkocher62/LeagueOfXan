﻿using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    internal static SfxController singleton;

    public AudioClip buttonClick;
    public AudioClip sprayPaint;
    public AudioClip throwItem;
    public AudioClip laserShoot;
    public AudioClip damageEntity;
    public AudioClip explosion;
    public AudioClip bigExplosion;
    public AudioClip sniperShot;

    private AudioSource source;

    private void Start()
    {
        singleton = this;

        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();
        ChangeVolume(SaveManager.saveData.sfxVolume);
    }

    internal void ChangeVolume(float volume) => source.volume = volume;

    public void PlayButtonClick()
    {
        source.clip = buttonClick;
        source.Play();
    }

    public void PlaySprayPaint()
    {
        source.clip = sprayPaint;
        source.Play();
    }

    public void PlayThrowItem()
    {
        source.clip = throwItem;
        source.Play();
    }

    public void PlayLaserShoot()
    {
        source.clip = laserShoot;
        source.Play();
    }

    public void PlaySniperShoot()
    {
        source.clip = sniperShot;
        source.Play();
    }

    public void PlayDamageEntity()
    {
        source.clip = damageEntity;
        source.Play();
    }

    public void PlayExplosion()
    {
        source.clip = explosion;
        source.Play();
    }

    public void PlayBigExplosion()
    {
        source.clip = bigExplosion;
        source.Play();
    }
}