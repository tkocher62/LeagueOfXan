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
		public Text totalKillsText;
		public Text totalDeathsText;
		public Text fastestTimeText;

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

			totalKillsText.text = $"Total Enemies Killed: {SaveManager.saveData.enemyKillCount}";
			totalDeathsText.text = $"Total Deaths: {SaveManager.saveData.deathCount}";

			if (SaveManager.saveData.fastestTime != -1)
			{
				TimeSpan t = TimeSpan.FromMilliseconds(SaveManager.saveData.fastestTime);
				string formatted = string.Format("{0:D2}:{1:D2}:{2:D3}",
						t.Minutes,
						t.Seconds,
						t.Milliseconds);
				fastestTimeText.text = $"Fastest Time: {formatted}";
			}

			foreach (Button button in GetComponentsInChildren<Button>(true))
			{
				button.onClick.AddListener(delegate { SfxController.singleton.PlayButtonClick(); });
			}
		}

		public void Back() => SceneManager.LoadScene(0);
	}
}
