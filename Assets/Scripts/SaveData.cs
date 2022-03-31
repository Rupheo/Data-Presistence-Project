using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveData
{
	public static void SaveGameData()
	{
		//String that contains our file paths
		string path = Application.persistentDataPath + "/savefile.json";

		//Create Save Data
		Data data = new Data();

		//List of Data to be save
		data.highPlayerName = HighScoreManager.Instance.PlayerName;
		data.highScore = HighScoreManager.Instance.HighScore;

		string saveFile = JsonUtility.ToJson(data);

		if (!File.Exists(path))
		{
			File.Create(path);
		}

		StreamWriter writer = new StreamWriter(path);
		writer.Write(saveFile);
		writer.Close();
	}

	public static void LoadGameData()
	{
		string path = Application.persistentDataPath + "/savefile.json";

		if (File.Exists(path))
		{
			StreamReader reader = new StreamReader(path);
			string file = reader.ReadToEnd();

			Data data = JsonUtility.FromJson<Data>(file);

			HighScoreManager.Instance.HighPlayerName = data.highPlayerName;
			HighScoreManager.Instance.HighScore = data.highScore;

			reader.Close();
		}
		else
		{
			HighScoreManager.Instance.HighPlayerName = "Anonymous";
			HighScoreManager.Instance.HighScore = 0;
		}			
	}
}

[System.Serializable]
class Data
{
	public string highPlayerName;
	public int highScore;
}
