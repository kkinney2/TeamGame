using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public GameObject player1;
    public GameObject player2;

    private Rigidbody rb;
    private bool isJumping1;
    private bool isJumping2;

    void Start()
    {
    }

    void FixedUpdate()
    {
        //Start Player1 Movement
        float moveHorizontal = Input.GetAxis("Horizontal P1");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        rb = player1.GetComponent<Rigidbody>();
        rb.AddForce(movement * speed);

        //Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.W) && !isJumping1)
        {
            rb.AddForce((Vector3.up * jumpHeight), ForceMode.Impulse);
            SetIsJumping("Player1", true);
        }
        //End Player1 Movement

        //Start Player2 Movement
        moveHorizontal = Input.GetAxis("Horizontal P2");
        movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        rb = player2.GetComponent<Rigidbody>();
        rb.AddForce(movement * speed);

        //Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping2)
        {
            rb.AddForce((Vector3.up * jumpHeight), ForceMode.Impulse);
            SetIsJumping("Player2" , true);
        }
        //End Player2 Movement
    }

    public void SetIsJumping(string playerName,bool isJumping )
    {
        if (playerName == "Player1")
        {
            isJumping1 = isJumping;
        }
        if (playerName == "Player2")
        {
            isJumping2 = isJumping;
        }
    }
}
