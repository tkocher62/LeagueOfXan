using Assets.Scripts.General;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
	public Joystick joystick;
	public GameObject confirmWindow;

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

	public void ConfirmExit()
	{
		if (isPaused)
		{
			Destroy(CentralUIController.singleton.gameObject);
			Destroy(CentralUIController.singleton.eventSystem);
			Destroy(PlayerController.singleton.gameObject);
			UnPause();
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
		if (isPaused) TimerController.StopTimer();
		else TimerController.StartTimer();
	}
}
