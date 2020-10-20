using System;
using System.Collections.Generic;

#pragma warning disable 0649

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
