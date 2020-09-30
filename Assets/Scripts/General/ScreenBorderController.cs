using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorderController : MonoBehaviour
{
    internal static Vector2 screenBounds;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
}
