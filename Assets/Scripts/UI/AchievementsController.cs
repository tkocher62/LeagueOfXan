using Assets.Scripts.General;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#pragma warning disable 0649

namespace Assets.Scripts.UI
{
	class AchievementsController : MonoBehaviour
	{
		[Serializable]
		public struct AchievementButton
		{
			public string id;
			public Image image;
		}

		public List<AchievementButton> achievements;

		private Color32 gray = new Color32(40, 40, 40, 100);

		private void Start()
		{
			foreach (AchievementButton ach in achievements)
			{
				if (!SaveManager.saveData.achievements.Contains(ach.id))
				{
					foreach (Image image in ach.image.gameObject.GetComponentsInChildren<Image>())
					{
						image.color = gray;
					}
				}
				else
				{
					Text text = ach.image.GetComponentInChildren<Text>();
					text.text = AchievementManager.achievementIDs[ach.id];
					text.fontSize = 49;
				}
			}
		}

		public void Back() => SceneManager.LoadScene(0);
	}
}
