using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    //added variable to hold HighScoreManager Instance references
    string m_HighPlayerName;
    string m_CurrentPlayerName;
    int m_HighScore; 

	// Start is called before the first frame update
	void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        //References to HighScoreManager.Instance varibles 
        m_HighPlayerName = HighScoreManager.Instance.HighPlayerName;
        m_CurrentPlayerName = HighScoreManager.Instance.PlayerName;
        m_HighScore = HighScoreManager.Instance.HighScore;

        HighScoreText.text = $"High Score : {m_HighPlayerName} :" +
            $" {m_HighScore}";
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                //SceneManager.LoadScene(0);  Unused
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        CheckForHighScore();

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    void CheckForHighScore()
	{
        if (m_Points > m_HighScore)
        {
            HighScoreManager.Instance.HighPlayerName = m_CurrentPlayerName;
            HighScoreManager.Instance.HighScore = m_Points;
            SaveData.SaveGameData();
        }
    }

    //Unused
    /*
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }*/
}
