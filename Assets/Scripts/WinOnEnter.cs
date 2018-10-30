using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinOnEnter : MonoBehaviour {

    private GameController gameController;

    private void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") && !gameController.GetRoundOver())
        {
            gameController.AddScore("Player1", 1);
        }
        if (other.CompareTag("Player2") && !gameController.GetRoundOver())
        {
            gameController.AddScore("Player2", 1);
        }
    }
}
