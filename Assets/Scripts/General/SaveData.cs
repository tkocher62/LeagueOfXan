using System;
using System.Collections.Generic;

namespace Assets.Scripts.General
{
	[Serializable]
	internal class SaveData
	{
		internal int enemyKillCount;
		internal int deathCount;
		internal int winCount;
		internal List<string> achievements = new List<string>();
	}
}
