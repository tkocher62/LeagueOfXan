using Assets.Scripts.General;
using UnityEngine;

public class GameMusicController : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        gameObject.GetComponent<AudioSource>().volume = SaveManager.saveData.musicVolume;
    }
}
