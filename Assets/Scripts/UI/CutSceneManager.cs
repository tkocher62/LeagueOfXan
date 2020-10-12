using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneManager : MonoBehaviour
{
    public VideoPlayer vid;

    private void Start()
    {
        vid.loopPointReached += CheckOver;
    }

    private void CheckOver(VideoPlayer vp)
    {
        print("video is over");
    }
}
