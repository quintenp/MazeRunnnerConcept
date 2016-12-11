using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D playerRigidBody;
    private RaycastHit2D groundRayCastHit;
    private RaycastHit2D roofRayCastHit;

    void Start () {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        groundRayCastHit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y - 2), 1 << LayerMask.NameToLayer("Ground"));
        roofRayCastHit = Physics2D.Linecast(transform.position, new Vector2(transform.position.x, transform.position.y + 2), 1 << LayerMask.NameToLayer("Roof"));
        
        if (Input.GetKey(KeyCode.Space))
        {
            if (roofRayCastHit)
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -5);
            }else
            {
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 5);
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerRigidBody.velocity = new Vector2(3, playerRigidBody.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            playerRigidBody.velocity = new Vector2(-3, playerRigidBody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            playerRigidBody.gravityScale = -3;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, 10);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            playerRigidBody.gravityScale = 3;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -10);
        }

        if (roofRayCastHit)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 180, -180), Time.deltaTime * 20);
        }

        if (groundRayCastHit)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 20);
        }
    }
}
