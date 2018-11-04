using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameController gameController;

    private Rigidbody2D rb;
    private bool isJumping;
    private Transform player2Spawn;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
        //Start Player2 Movement
        float moveHorizontal = Input.GetAxis("Horizontal_P2");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        rb.velocity = movement * speed;

        //Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
            SetIsJumping(true);
        }
        //End Player2 Movement

    }

    public void SetIsJumping(bool isJumpingTemp)
    {
        isJumping = isJumpingTemp;
    }

    public void Respawn()
    {
        transform.position = gameController.GetPlayerSpawn(tag);
        rb.velocity = Vector3.zero;

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            SetIsJumping(false);
        }
        if (other.gameObject.CompareTag("Hazard"))
        {
            Respawn();
        }
    }
}
