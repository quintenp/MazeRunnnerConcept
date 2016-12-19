using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D playerRigidBody;
    private RaycastHit2D groundRayCastHit;
    private RaycastHit2D roofRayCastHit;

    private bool flipUp;
    private bool flipDown;
    private bool isFacingRight;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        isFacingRight = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (flipUp)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -5);
            }
            else
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 5);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            isFacingRight = true;
            playerRigidBody.velocity = new Vector2(5, playerRigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            isFacingRight = false;
            playerRigidBody.velocity = new Vector2(-5, playerRigidBody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            flipDown = false;
            flipUp = true;
            playerRigidBody.gravityScale = -3;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 10);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            flipDown = true;
            flipUp = false;
            playerRigidBody.gravityScale = 3;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -10);
        }

        if (flipUp)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, -180), Time.deltaTime * 10);
        }

        if (flipDown)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 10);
        }

        Flip();
    }

    void Flip()
    {
        if (isFacingRight == true)
        {
            transform.localScale = new Vector3(.5f, .5f, 0);
        }
        if (isFacingRight == false)
        {
            transform.localScale = new Vector3(-.5f, .5f, 0);
        }
    }
}
