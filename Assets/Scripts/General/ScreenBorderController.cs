using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorderController : MonoBehaviour
{
    private void Start() => DontDestroyOnLoad(gameObject);
}
