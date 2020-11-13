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

		internal static Vector2 Vector2FromAngle(float a)
		{
			a *= Mathf.Deg2Rad;
			return new Vector2(Mathf.Cos(a), Mathf.Sin(a));
		}
	}
}
