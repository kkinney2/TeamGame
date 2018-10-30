using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachable : MonoBehaviour {

    private PlayerController playerController;

    private void Start()
    {
        GameObject playerControllerObject = GameObject.FindWithTag("PlayerController");
        if (playerControllerObject != null)
        {
            playerController = playerControllerObject.GetComponent<PlayerController>();
        }
        if (playerController == null)
        {
            Debug.Log("Cannot find 'PlayerController' script");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            playerController.SetIsJumping(this.tag, false);
        }
    }
}
