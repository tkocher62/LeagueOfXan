using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject musicSlider;

    private Slider slider;
    private AudioSource musicController;

    private void Start()
    {
        slider = musicSlider.GetComponent<Slider>();

        musicController = GameObject.Find("MusicController").GetComponent<AudioSource>();
    }

    public void ChangeVolume()
    {
        musicController.volume = slider.value;
    }
}
