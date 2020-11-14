using Assets.Scripts.General;
using UnityEngine;

public class BossMusicController : MonoBehaviour
{
    void Start() => gameObject.GetComponent<AudioSource>().volume = SaveManager.saveData.musicVolume;
}
