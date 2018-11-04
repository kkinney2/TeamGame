using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public GameObject player1;
    public GameObject player2;
    public GameController gameController;

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    private bool isJumping1;
    private bool isJumping2;
    private Transform player1Spawn;
    private Transform player2Spawn;

    private void Start()
    {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();
        StartCoroutine(SpawnWait());
    }

    IEnumerator SpawnWait()
    {
        yield return new WaitForSeconds(1);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
    void FixedUpdate()
    {
        //Start Player1 Movement
        float moveHorizontal = Input.GetAxis("Horizontal_P1");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        rb1.velocity = movement * speed;

        //Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.W) && !isJumping1)
        {
            rb1.velocity = Vector3.up * jumpHeight;
            SetIsJumping("Player1", true);
        }
        //End Player1 Movement

        //Start Player2 Movement
        moveHorizontal = Input.GetAxis("Horizontal_P2");
        movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        rb2.velocity = movement * speed;

        //Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping2)
        {
            rb2.velocity = Vector3.up * jumpHeight;
            SetIsJumping("Player2" , true);
        }
        //End Player2 Movement
    }

    public void SetIsJumping(string tag, bool isJumping )
    {
        if (tag == "Player1")
        {
            isJumping1 = isJumping;
        }
        if (tag == "Player2")
        {
            isJumping2 = isJumping;
        }
    }

    public void Respawn(string tag)
    {
        if (tag == "Player1")
        {
            rb1.position = gameController.GetPlayerSpawn(tag);
            rb1.velocity = Vector3.zero;
        }
        if (tag == "Player2")
        {
            rb2.position = gameController.GetPlayerSpawn(tag);
            rb2.velocity = Vector3.zero;
        }
    }

}
