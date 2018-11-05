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
    private bool roundOver;
    private bool isLevelLoaded;
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
        isLevelLoaded = true;

        StartCoroutine(LevelInfo());
        StartCoroutine(ShuffleLevels());
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
                SceneManager.LoadScene("TumbleTemple2D", LoadSceneMode.Single);
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
                if (!isLevelLoaded)
                {
                    currentLevel = levelPrefabs[Random.Range(0, levelPrefabs.Length)];
                    StartCoroutine(LevelInfo());
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(currentLevel, currentLevelScript.GetLevelSpawn(), spawnRotation);
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < players.Length; i++)
                {
                    GameObject player = players[i];
                    Debug.Log("Player: " + player);
                    Vector2 playerSpawnValue = currentLevelScript.GetPlayerSpawn(i+1);
                    player.transform.position = playerSpawnValue;
                    Debug.Log("PlayerSpawnValue:" + playerSpawnValue);
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

    IEnumerator LevelInfo()
    {
        currentLevelScript = currentLevel.GetComponent<Level>();
        GameObject[] children = currentLevel.GetComponentsInChildren<GameObject>();
        foreach (GameObject child in children)
        {
            if (child.name == "DropBlock1")
            {
                currentLevelScript.SetDropBlock(1, child);
            }
            if (child.name == "DropBlock2")
            {
                currentLevelScript.SetDropBlock(2, child);
            }
            if (child.name == "Player1_Spawn")
            {
                currentLevelScript.SetPlayerSpawn(1, child.transform.position);
            }
            if (child.name == "Player2_Spawn")
            {
                currentLevelScript.SetPlayerSpawn(2, child.transform.position);
            }
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

        //Start Blocks Destroy
        for (int i = 1; i < 3; i++)
        {
            Destroy(currentLevelScript.GetDropBlock(i));
        }

        player1InformText.text = "GO!";
        player2InformText.text = "GO!";
    }

    private GameObject[] LevelsShuffle(GameObject[] array)
    {
        //Durstenfeld Shuffle
        for (var i = array.Length - 1; i > 0; i--)
        {
            int j = (int)Mathf.Floor(Random.value * (i + 1));
            GameObject temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
        return array;
    }
    IEnumerator ShuffleLevels()
    {
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