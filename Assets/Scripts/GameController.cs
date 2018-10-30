using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject[] levels;
    public GameObject[] players;
    public GameObject[] playerSpawns;
    public GameObject[] destroyObjects;
    public Vector3 levelSpawnValues;
    public int levelCount;
    public float levelSpawnWait;
    public float startWait;
    public float waveWait;
    public int maxRounds = 1;
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
            for (int rounds = 0; rounds < maxRounds; rounds++)
            {
                roundOver = false;

                GameObject level = levels[Random.Range(0, levels.Length)];
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(level, levelSpawnValues, spawnRotation);
                //for (int i = 0; i< players.Length; i++)
                //{
                //    GameObject player = players[i];
                //    GameObject playerSpawn = playerSpawns[i];
                //    Vector3 playerSpawnValue = playerSpawn.transform.position;
                //    Instantiate(player, playerSpawnValue, spawnRotation);
                //}
                yield return new WaitForSeconds(3);
                for(int i =0; i < destroyObjects.Length; i++)
                {
                    Destroy(destroyObjects[i]);
                }
                while (!roundOver)
                {
                    yield return new WaitForSeconds(1);
                }
            }
            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(string playerName, int addScoreValue)
    {
        if (playerName == "Player1")
        {
            score1 += addScoreValue;
        }
        if (playerName == "Player2")
        {
            score2 += addScoreValue;
        }
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText1.text = "" + score1;
        scoreText2.text = "" + score2;
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

    public bool GetRoundOver()
    {
        return roundOver;
    }

}
