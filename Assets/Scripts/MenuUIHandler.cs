#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]

public class MenuUIHandler : MonoBehaviour
{
	[SerializeField] TMP_InputField nameInput;

	public void StartGame()
	{
		if (nameInput.text == "")
			HighScoreManager.Instance.PlayerName = "Anonymous";
		else
			HighScoreManager.Instance.PlayerName = nameInput.text;

		SceneManager.LoadScene(1);
	}

	public void Exit()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}
}
