﻿using Assets.Scripts.General;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    internal static class AchievementManager
    {
        internal static Dictionary<string, string> achievementIDs = new Dictionary<string, string>()
		{
			{"play_the_game", "Play the game" },
			{"beat_the_game", "Beat the game" },
			{"die_100_times", "Die 100 times" },
			{"kill_100_enemies", "Kill 100 enemies" }
        };

        internal static void Achieve(string id)
        {
            if (achievementIDs.ContainsKey(id))
            {
                if (!SaveManager.saveData.achievements.Contains(id))
                {
                    SaveManager.saveData.achievements.Add(id);
                    //Debug.Log("Achievement unlocked! " + id);
                    SaveManager.SaveData();
                }
            }
            else
            {
                Debug.LogError("Error finding achievement with id: " + id);
            }
        }
    }
}
