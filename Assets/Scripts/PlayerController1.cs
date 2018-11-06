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

    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;

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
        if (gameController.HasMovement())
        {
            float moveHorizontal = Input.GetAxisRaw("Horizontal_P1") * speed;
            Move(moveHorizontal * Time.fixedDeltaTime);

            //Jumping Mechanic
            if (Input.GetKeyDown(KeyCode.W) && !isJumping)
            {
                Jump();
            }
        }
    }

    void Move(float move)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, rb2D.velocity.y);
        // And then smoothing it out and applying it to the character
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
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
