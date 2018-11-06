using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Mechanics based from 'Brackeys' 2D Character Controller
public class PlayerController2 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameController gameController;

    private Rigidbody2D rb2D;
    private bool isJumping = false;
    private Transform player2Spawn;
    private bool isFacingRight;

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
            float moveHorizontal = Input.GetAxisRaw("Horizontal_P2") * speed;
            Move(moveHorizontal * Time.fixedDeltaTime);

            //Jumping Mechanic
            if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
            {
                Jump();
            }
        }
    }

    void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (move > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (move < 0 && isFacingRight)
        {
            Flip();
        }
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

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
