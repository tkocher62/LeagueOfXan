using System;
using System.Collections.Generic;

#pragma warning disable 0649

namespace Assets.Scripts.General
{
	[Serializable]
	internal class SaveData
	{
		internal int enemyKillCount = 0;
		internal int deathCount = 0;
		internal long fastestTime = -1;
		internal List<string> achievements = new List<string>();
	}
}
