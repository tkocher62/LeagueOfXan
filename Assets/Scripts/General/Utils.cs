using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.General
{
	internal static class Utils
	{
		internal const float scale = 0.7f;

		internal static GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
		{
			GameObject obj = GameObject.Instantiate(original, position, rotation);
			
			// If on boss stage
			if (SceneManager.GetActiveScene().buildIndex == 12)
			{
				obj.transform.localScale *= scale;
			}
			return obj;
		}
	}
}
