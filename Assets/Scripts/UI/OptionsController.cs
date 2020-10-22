using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject musicSliderObject;
    public GameObject deleteDataConfirmPopup;
    public GameObject easyModeCheckbox;

    private Slider musicSlider;
    private AudioSource musicController;
    private Toggle easyModeToggle;

    private void Start()
    {
        musicSlider = musicSliderObject.GetComponent<Slider>();

        musicController = GameObject.Find("MusicController").GetComponent<AudioSource>();

        easyModeToggle = easyModeCheckbox.GetComponent<Toggle>();

        musicSlider.value = SaveManager.saveData.musicVolume;

        easyModeToggle.isOn = SaveManager.saveData.isEasyMode;
    }

    public void ChangeVolume()
    {
        musicController.volume = musicSlider.value;
    }

    public void BackButton()
    {
        SaveManager.saveData.musicVolume = musicSlider.value;
        // save SFX volume when implemented

        SaveManager.SaveData();

        SceneManager.LoadScene(0);
    }

    public void ClearSaveDataPopup(bool show)
    {
        deleteDataConfirmPopup.SetActive(show);
    }

    public void ConfirmDeleteSaveData()
    {
        SaveManager.DeleteSaveData();
        ClearSaveDataPopup(false);
    }

    public void SetEasyMode()
    {
        if (SaveManager.saveData.isEasyMode != easyModeToggle.isOn)
        {
            SaveManager.saveData.isEasyMode = easyModeToggle.isOn;
            SaveManager.SaveData();
        }
    }
}
