using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] levelPrefabs;
    public int levelCount;
    public float levelSpawnWait;
    public float startWait;
    public int maxRounds = 1;
    public Text scoreText1;
    public Text scoreText2;
    public Text restartText;
    public Text gameOverText;
    public Text player1InformText;
    public Text player2InformText;

    private bool gameOver;
    private bool restart;
    private bool roundOver;
    private int score1;
    private int score2;
    private Level currentLevelScript;

    private void Start()
    {
        gameOver = false;
        restart = false;
        //restartText.text = "";
        //gameOverText.text = "";
        score1 = 0;
        score2 = 0;
        UpdateScore();

        for (int i = 0; i < levelPrefabs.Length; i++)
        {
            
        }

        

        StartCoroutine(SpawnLevels());
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

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

                GameObject currentLevel = levelPrefabs[Random.Range(0, levelPrefabs.Length)];
                Quaternion spawnRotation = Quaternion.identity;

                currentLevelScript = currentLevel.GetComponent<Level>();

                Instantiate(currentLevel, currentLevelScript.GetLevelSpawn(), spawnRotation);

                for (int i = 1; i < players.Length; i++)
                {
                    GameObject player = players[i];
                    Vector3 playerSpawnValue = currentLevelScript.GetPlayerSpawn(i);
                    Instantiate(player, playerSpawnValue, spawnRotation);
                }

                StartCoroutine(StartLevel());
                yield return new WaitForSeconds(3);
                
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

    IEnumerator StartLevel()
    {
        //Countdown
        player1InformText.text = "Ready?";
        player2InformText.text = "Ready?";
        yield return new WaitForSeconds(1.5f);
        player1InformText.text = "Set!";
        player2InformText.text = "Set!";
        yield return new WaitForSeconds(1.5f);

        //Start Blocks Destroy
        for (int i = 1; i < 3; i++)
        {
            Destroy(currentLevelScript.GetDropBlock(i));
        }

        player1InformText.text = "GO!";
        player2InformText.text = "GO!";
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

    public Vector2 GetPlayerSpawn(string tag)
    {
        if (tag == "Player1")
        {
            return currentLevelScript.GetPlayerSpawn(1);
        }
        if (tag == "Player2")
        {
            return currentLevelScript.GetPlayerSpawn(2);
        }
        else return new Vector2 (0,0);
    }
}