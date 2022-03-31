using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//redo this again
public class HighScoreManager : MonoBehaviour
{
	public static HighScoreManager instance;

	public string PlayerName;
	public string HighPlayerName;
	public int HighScore;

	private void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);

		//LoadHightScore();
		SaveData.LoadGameData();
	}

	public static HighScoreManager Instance
	{
		get { return instance; }
	}
}
