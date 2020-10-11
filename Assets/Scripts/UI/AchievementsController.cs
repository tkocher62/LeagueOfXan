using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
	class AchievementsController : MonoBehaviour
	{
		public void Back() => SceneManager.LoadScene(0);
	}
}
