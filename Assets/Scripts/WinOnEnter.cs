using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnEnter : MonoBehaviour {

    private GameController gameController;
    private bool hasEnded;

    private void Start()
    {
        hasEnded = false;

        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") && (gameController.GetRoundOver() == false) && !hasEnded)
        {
            gameController.AddScore(other.tag, 1);
        }
        if (other.CompareTag("Player2") && (gameController.GetRoundOver() == false) && !hasEnded)
        {
            gameController.AddScore(other.tag, 1);
        }
        if (hasEnded == false)
        {
            StartCoroutine(RoundOver());
        }

        hasEnded = true;
    }
    IEnumerator RoundOver()
    {
        gameController.player1InformText.text = "Round Over!";
        gameController.player2InformText.text = "Round Over!";
        yield return new WaitForSeconds(3);
        gameController.player1InformText.text = "";
        gameController.player2InformText.text = "";
        gameController.RoundOver();
    }
}
