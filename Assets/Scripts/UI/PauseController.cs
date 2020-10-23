﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
	public Joystick joystick;

	internal static bool isPaused = false;

	public void Pause()
	{
		if (!isPaused)
		{
			gameObject.SetActive(true);
			TogglePause();
			joystick.HandleRange = 0f;
		}
	}

	public void UnPause()
	{
		if (isPaused)
		{
			gameObject.SetActive(false);
			TogglePause();
			joystick.HandleRange = 1f;
		}
	}

	public void MainMenu()
	{
		if (isPaused)
		{
			Destroy(CanvasController.singleton.gameObject);
			Destroy(CanvasController.singleton.eventSystem);
			Destroy(PlayerController.singleton.gameObject);
			UnPause();
			SceneManager.LoadScene(0);
		}
	}

	private void TogglePause()
	{
		Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
		isPaused = !isPaused;
	}
}
