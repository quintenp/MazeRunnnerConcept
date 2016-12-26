using HedgehogTeam.EasyTouch;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform GroundCheck;
    public Transform WallCheck;

    public AudioSource JumpSound;
    public AudioSource OuchSound;
    public AudioSource FlipSound;

    public float JumpForce;
    public float FlipForce;
    public float MovementSpeed;
    public int moveDirection;

    private GameManager gameManager;
    private Rigidbody2D playerRigidBody;
    private Animator playerAnimator;

    private bool flipUp;
    private bool flipDown;
    private bool isFacingRight;
    private bool grounded;
    private bool walking;

    public string NextLevel;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        moveDirection = 0;
        flipDown = true;
        flipUp = false;
        isFacingRight = true;
        walking = false;
        EasyTouch.On_SwipeEnd += On_SwipeEnd;
        EasyTouch.On_SimpleTap += On_SimpleTap;
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, 1 << LayerMask.NameToLayer("Ground")) || Physics2D.OverlapCircle(GroundCheck.position, 0.1f, 1 << LayerMask.NameToLayer("Roof"));

        if (grounded)
        {
            playerRigidBody.velocity = new Vector2(MovementSpeed * moveDirection, playerRigidBody.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            JumpSound.Play();

            if (flipUp)
            {
                playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, -JumpForce), ForceMode2D.Impulse);
            }
            else
            {
                playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, JumpForce), ForceMode2D.Impulse);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            walking = true;
            isFacingRight = true;
            moveDirection = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            walking = true;
            isFacingRight = false;
            moveDirection = -1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            FlipSound.Play();
            flipDown = false;
            flipUp = true;
            playerRigidBody.gravityScale = -9;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, FlipForce);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            FlipSound.Play();
            flipDown = true;
            flipUp = false;
            playerRigidBody.gravityScale = 9;
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -FlipForce);
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

        playerAnimator.SetBool("Grounded", grounded);
        playerAnimator.SetBool("Walking", walking);
    }

    void Flip()
    {
        if (isFacingRight == true)
        {
            transform.localScale = new Vector3(.6f, .6f, 0);
        }
        if (isFacingRight == false)
        {
            transform.localScale = new Vector3(-.6f, .6f, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Spike"))
        {
            OuchSound.Play();
            gameManager.PlayerDeathPosition = playerRigidBody.position;
            gameManager.ResetGame();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag.Equals("Exit"))
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(NextLevel);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            moveDirection = 0;
            walking = false;
            isFacingRight = !isFacingRight;
        }
    }

    void On_SwipeEnd(Gesture gesture)
    {
        switch (gesture.swipe)
        {
            case EasyTouch.SwipeDirection.DownLeft:
            case EasyTouch.SwipeDirection.UpLeft:
            case EasyTouch.SwipeDirection.Left:
                walking = true;
                isFacingRight = false;
                moveDirection = -1;
                break;
            case EasyTouch.SwipeDirection.DownRight:
            case EasyTouch.SwipeDirection.UpRight:
            case EasyTouch.SwipeDirection.Right:
                walking = true;
                isFacingRight = true;
                moveDirection = 1;
                break;
            case EasyTouch.SwipeDirection.Up:
                FlipSound.Play();
                flipDown = false;
                flipUp = true;
                playerRigidBody.gravityScale = -9;
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, FlipForce);
                break;
            case EasyTouch.SwipeDirection.Down:
                FlipSound.Play();
                flipDown = true;
                flipUp = false;
                playerRigidBody.gravityScale = 9;
                playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -FlipForce);
                break;
        }
    }

    void On_SimpleTap(Gesture gesture)
    {
        JumpSound.Play();

        if (flipUp)
        {
            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, -JumpForce), ForceMode2D.Impulse);
         //   playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, -JumpForce);
        }
        else
        {
            playerRigidBody.AddForce(new Vector2(playerRigidBody.velocity.x, JumpForce), ForceMode2D.Impulse);
            //playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, JumpForce);
        }
    }

    void OnDestroy()
    {
        EasyTouch.On_SwipeEnd -= On_SwipeEnd;
        EasyTouch.On_SimpleTap -= On_SimpleTap;
    }

    void OnDisable()
    {
        EasyTouch.On_SwipeEnd -= On_SwipeEnd;
        EasyTouch.On_SimpleTap -= On_SimpleTap;
    }
}
