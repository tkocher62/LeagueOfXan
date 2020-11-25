using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class BossIntroController : MonoBehaviour
{
    public VideoPlayer vid;

    private void Start()
    {
        PlayerController.singleton.gameObject.SetActive(false);
        PlayerController.singleton.joystick.HandleRange = 0f;
        PlayerController.singleton.movement = Vector2.zero;
        CentralUIController.singleton.gameObject.SetActive(false);

        vid.loopPointReached += CheckOver;
        vid.SetDirectAudioVolume(0, SaveManager.saveData.musicVolume);
    }

    private void CheckOver(VideoPlayer vp)
    {
        PlayerController.singleton.gameObject.SetActive(true);
        PlayerController.singleton.joystick.HandleRange = 1f;
        CentralUIController.singleton.gameObject.SetActive(true);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
