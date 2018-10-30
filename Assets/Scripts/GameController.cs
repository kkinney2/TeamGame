using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] levels;
    public Vector3 levelSpawnValues;
    public int levelCount;
    public float levelSpawnWait;
    public float startWait;
    public float waveWait;
    public int numRounds = 1;
    public Text scoreText1;
    public Text scoreText2;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private bool roundOver;
    private int score1;
    private int score2;

    private void Start()
    {
        gameOver = false;
        restart = false;
        //restartText.text = "";
        //gameOverText.text = "";
        score1 = 0;
        score2 = 0; 
        UpdateScore();
        StartCoroutine(SpawnLevels());
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel (Application.loadedLevel);
                SceneManager.LoadScene("TumbleTemple", LoadSceneMode.Single);
            }
        }
    }

    IEnumerator SpawnLevels()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int rounds = 0; rounds < numRounds; rounds++)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    break;
                }
            }
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                break;
            }
        }
    }

    public void AddScore(string playerName, int newScoreValue)
    {
        if (playerName == "Player1")
        {
            score1 += newScoreValue;
        }
        if (playerName == "Player2")
        {
            score2 += newScoreValue;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        //scoreText1.text = " Player1 Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
    public void RoundOver()
    {
        roundOver = true;
    }
}
