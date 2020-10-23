using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
	public Joystick joystick;
	public GameObject confirmWindow;

	internal static bool isPaused = false;

	public void SetPauseStatus(bool paused)
	{
		gameObject.SetActive(paused);
		TogglePause();
		joystick.HandleRange = paused ? 0f : 1f;
	}

	public void ConfirmExit()
	{
		if (isPaused)
		{
			Destroy(CanvasController.singleton.gameObject);
			Destroy(CanvasController.singleton.eventSystem);
			Destroy(PlayerController.singleton.gameObject);
			SetPauseStatus(false);
			SceneManager.LoadScene(0);
		}
	}

	public void RejectExit()
	{
		confirmWindow.SetActive(false);
	}

	public void MainMenu()
	{
		confirmWindow.SetActive(true);
	}

	private void TogglePause()
	{
		Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
		isPaused = !isPaused;
	}
}
