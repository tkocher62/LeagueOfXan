using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject musicSlider;
    public GameObject deleteDataConfirmPopup;
    public GameObject easyModeCheckbox;

    private Slider slider;
    private AudioSource musicController;
    private Toggle easyModeToggle;

    private void Start()
    {
        slider = musicSlider.GetComponent<Slider>();

        musicController = GameObject.Find("MusicController").GetComponent<AudioSource>();

        easyModeToggle = easyModeCheckbox.GetComponent<Toggle>();

        slider.value = musicController.volume;

        easyModeToggle.isOn = SaveManager.saveData.isEasyMode;
    }

    public void ChangeVolume()
    {
        musicController.volume = slider.value;
    }

    public void BackButton()
    {
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
            Debug.Log("set easy mode " + easyModeToggle.isOn);
        }
    }
}
