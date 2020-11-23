using Assets.Scripts.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject musicSliderObject;
    public GameObject sfxSliderObject;
    public GameObject deleteDataConfirmPopup;
    public GameObject easyModeCheckbox;

    private Slider musicSlider;
    private Slider sfxSlider;
    private AudioSource musicController;
    private Toggle easyModeToggle;
    private SfxController sfx;

    private void Start()
    {
        musicSlider = musicSliderObject.GetComponent<Slider>();
        sfxSlider = sfxSliderObject.GetComponent<Slider>();

        musicController = GameObject.Find("MusicController").GetComponent<AudioSource>();

        easyModeToggle = easyModeCheckbox.GetComponent<Toggle>();

        sfx = FindObjectOfType<SfxController>();
        foreach (Button button in GetComponentsInChildren<Button>(true))
        {
            button.onClick.AddListener(delegate { sfx.ButtonClickSound(); });
        }

        musicSlider.value = SaveManager.saveData.musicVolume;
        sfxSlider.value = SaveManager.saveData.sfxVolume;

        easyModeToggle.isOn = SaveManager.saveData.isEasyMode;
    }

    public void ChangeMusicVolume() => musicController.volume = musicSlider.value;

    public void ChangeSfxVolume() => sfx.ChangeVolume(sfxSlider.value);

    public void BackButton()
    {
        SaveManager.saveData.musicVolume = musicSlider.value;
        SaveManager.saveData.sfxVolume = sfxSlider.value;

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
