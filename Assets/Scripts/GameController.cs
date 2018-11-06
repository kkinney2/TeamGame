using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] levelPrefabs;
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
    private bool isRoundOver;
    private bool isLevelLoaded;
    private bool hasLevelStarted;
    private bool allowMovement;
    private int score1;
    private int score2;
    private Level currentLevelScript;
    private GameObject currentLevel;
    private GameObject[] levels;

    private void Start()
    {
        gameOver = false;
        restart = false;
        //restartText.text = "";
        //gameOverText.text = "";
        player1InformText.text = "";
        player2InformText.text = "";
        score1 = 0;
        score2 = 0;
        UpdateScore();
        isRoundOver = false;
        isLevelLoaded = false;
        hasLevelStarted = false;
        allowMovement = false;

        currentLevel = levelPrefabs[0];
        
        if (currentLevel != null)
        {
            currentLevelScript = currentLevel.GetComponent<Level>();
        }
        if (currentLevel == null)
        {
            Debug.Log("Cannot find 'CurrentLevel' script");
        }
        
        currentLevel = levelPrefabs[0];
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(currentLevel, currentLevelScript.GetLevelSpawn(), spawnRotation);
        isLevelLoaded = true;

        StartCoroutine(ShuffleLevels(levelPrefabs));
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        if (gameOver)
        {
            restartText.text = "Press 'R' for Restart";
            restart = true;
        }

        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Application.LoadLevel (Application.loadedLevel);
                SceneManager.LoadScene("TumbleTemple2D", LoadSceneMode.Single);
            }
        }
    }

    private void FixedUpdate()
    {
        //Level Sequencing
        if (!gameOver)
        {
            if (!hasLevelStarted)
            {
                hasLevelStarted = true;
                StartCoroutine(StartLevel());
            }
            if (isRoundOver)
            {
                allowMovement = false;
                Destroy(currentLevel);
                isLevelLoaded = false;
                StartCoroutine(SpawnLevel());
                StartCoroutine(SpawnPlayers());
                isRoundOver = false;
            }
        }
    }

    IEnumerator SpawnLevel()
    {
        if (!isLevelLoaded)
        {
            currentLevel = levels[Random.Range(0, levelPrefabs.Length)];
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(currentLevel, currentLevelScript.GetLevelSpawn(), spawnRotation);
            isLevelLoaded = true;
        }
        if (isLevelLoaded)
        {
            Debug.Log("A level is already loaded");
        }
        yield return new WaitForSeconds(1);
    }

    IEnumerator SpawnPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject player = players[i];
            Debug.Log("Player: " + player);
            Vector2 playerSpawnValue = currentLevelScript.GetPlayerSpawn(i + 1);
            player.transform.position = playerSpawnValue;
            Debug.Log("PlayerSpawnValue:" + playerSpawnValue);
        }
        yield return new WaitForSeconds(1);
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

        player1InformText.text = "GO!";
        player2InformText.text = "GO!";
        allowMovement = true;
        yield return new WaitForSeconds(1.5f);

        player1InformText.text = "";
        player2InformText.text = "";
    }

    private GameObject[] LevelsShuffle(GameObject[] array)
    {
        //Durstenfeld Shuffle
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = (int)Mathf.Floor(Random.value * (i + 1));
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        return array;
    }

    IEnumerator ShuffleLevels(GameObject[] levels)
    {
        if(levels.Length == 0)
        {
            levels = levelPrefabs;
        }
        LevelsShuffle(levels);
        yield return new WaitForSeconds(1);
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
        isRoundOver = true;
    }

    public bool GetRoundOver()
    {
        return isRoundOver;
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

    public bool HasMovement()
    {
        return allowMovement;
    }
}