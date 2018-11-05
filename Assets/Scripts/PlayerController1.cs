using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameController gameController;

    private Rigidbody2D rb2D;
    private bool isJumping = false;
    private Transform player1Spawn;

    private void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
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
        float moveHorizontal = Input.GetAxis("Horizontal_P1");
        Move(moveHorizontal);

        //Jumping Mechanic
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            Jump();
        }
    }

    void Move(float moveHorizontal)
    {
        Vector2 movement = new Vector2(moveHorizontal, 0.0f);

        rb2D.velocity = movement * speed;
    }

    void Jump()
    {
        rb2D.AddForce(new Vector2(0.0f, jumpForce));
        isJumping = true;
    }

    public void Respawn()
    {
        transform.position = gameController.GetPlayerSpawn(tag);
        rb2D.velocity = Vector3.zero;
           
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            isJumping = false;
        }
        if (other.gameObject.CompareTag("Hazard"))
        {
            Respawn();
        }
        else
        {
            //Debug.Log("Collision Not found");
        }
    }
}
