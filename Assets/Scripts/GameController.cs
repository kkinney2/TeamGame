using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] levelPrefabs;
    public int maxRounds = 1;
    public Text scoreTitleText1;
    public Text scoreTitleText2;
    public Text scoreText1;
    public Text scoreText2;
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
        player1InformText.text = "";
        player2InformText.text = "";
        scoreTitleText1.text = "Player 1 Score:";
        scoreTitleText2.text = "Player 2 Score:";
        score1 = 0;
        score2 = 0;
        UpdateScore();
        isRoundOver = false;
        isLevelLoaded = false;
        hasLevelStarted = false;
        allowMovement = false;

        levels = levelPrefabs;
        LevelsShuffle(levels);

        SpawnLevel();
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
                SceneManager.LoadScene("TumbleTemple2D", LoadSceneMode.Single);
            }
        }

        //Level Sequencing
        if (!gameOver)
        {
            if ((score1 + score2) >= maxRounds)
            {
                GameOver();
            }
            if (!hasLevelStarted && score1 + score2 <= maxRounds)
            {
                hasLevelStarted = true;
                StartCoroutine(StartLevel());
            }
            if (isRoundOver)
            {
                allowMovement = false;
                isLevelLoaded = false;
                isRoundOver = false;
                hasLevelStarted = false;
                if(currentLevelScript != null)
                {
                    currentLevelScript.DestroyLevel();
                }
                if (currentLevelScript == null)
                {
                    FindCurrentLevelScript();
                    currentLevelScript.DestroyLevel();
                }
                currentLevel = null;
                if (!gameOver)
                {
                    SpawnLevel();
                    SpawnPlayers();
                }
            }
        }
    }

    private void SpawnLevel()
    {
        if (isLevelLoaded == true)
        {
            Debug.Log("A level is already loaded");
        }
        if (isLevelLoaded == false)
        {
            isLevelLoaded = true;
            currentLevel = levels[Random.Range(0, levels.Length-1)];
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(currentLevel, new Vector3 (0,0,0), spawnRotation);
            Debug.Log("Level Loaded: " + currentLevel);
            FindCurrentLevelScript();
        }
    }

    private void FindCurrentLevelScript()
    {
        GameObject currentLevelTemp = GameObject.FindWithTag("Level");
        if (currentLevelTemp != null)
        {
            currentLevelScript = currentLevelTemp.GetComponent<Level>();
        }
        if (currentLevelTemp == null)
        {
            Debug.Log("Cannot find 'CurrentLevel' script");
        }
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            GameObject player = players[i];
            //Debug.Log("Player: " + player);
            Vector2 playerSpawnValue = currentLevelScript.GetPlayerSpawn(i + 1);
            player.transform.position = playerSpawnValue;
            //Debug.Log("PlayerSpawnValue:" + playerSpawnValue);
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
        scoreTitleText1.text = "Game Over!";
        scoreTitleText2.text = "";
        if (score1 > score2)
        {
            scoreText1.text = "Player 1 Wins!";
        }
        if (score1 < score2)
        {
            scoreText1.text = "Player 2 Wins!";
        }
        scoreText2.text = "Press 'R' for Restart";
        restart = true;
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