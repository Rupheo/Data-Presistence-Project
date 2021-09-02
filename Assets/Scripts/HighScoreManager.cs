using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//redo this again
public class HighScoreManager : MonoBehaviour
{
	public static HighScoreManager Instance;

	public string PlayerName;
	public int HighScore;

	private void Awake()
	{
		if(Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	[System.Serializable]
	class SaveData
	{
		public string Name;
		public int Score;
	}

	public void SaveHighScore()
	{
		SaveData data = new SaveData();
		data.Name = PlayerName;
		data.Score = HighScore;

		string json = JsonUtility.ToJson(data);

		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
	}

	public void LoadHightScore()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SaveData data = JsonUtility.FromJson<SaveData>(json);

			PlayerName = data.Name;
			HighScore = data.Score;
		}
	}
}
