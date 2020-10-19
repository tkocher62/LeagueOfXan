using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.General
{
	internal static class SaveManager
	{
		internal static string path = Application.persistentDataPath + "/data.xan";
		internal static SaveData saveData = new SaveData();

		internal static void SaveData()
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Create);
			formatter.Serialize(stream, saveData);
			stream.Close();
		}

		internal static void LoadData()
		{
			if (File.Exists(path))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				FileStream stream = new FileStream(path, FileMode.Open);
				saveData = (SaveData)formatter.Deserialize(stream);
				stream.Close();
			}
			else
			{
				Debug.LogError("Save file not found in " + path);
			}
		}
	}
}
