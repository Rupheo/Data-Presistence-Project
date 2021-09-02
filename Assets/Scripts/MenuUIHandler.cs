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
	public TMP_InputField nameInput;
	private string text;

	private void Update()
	{
			text = nameInput.GetComponent<TMP_InputField>().text;
	}

	public void StartGame()
	{
		if (text == "")
			HighScoreManager.Instance.PlayerName = "Anonymous";
		else
			HighScoreManager.Instance.PlayerName = nameInput.GetComponent<TMP_InputField>().text;

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
