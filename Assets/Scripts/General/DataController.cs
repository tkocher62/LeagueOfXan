using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerController;

public class DataController : MonoBehaviour
{
    internal static float health = 100f;
    internal static Character character = Character.Xan;

    void Start() => DontDestroyOnLoad(gameObject);
}
