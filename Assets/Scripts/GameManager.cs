using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public ParticleSystem DeathParticle;
    public Vector3 PlayerDeathPosition;

    private LevelTimer levelTimer;
    private Vector3 playerStartPosition;
    private Rigidbody2D playerRigidBody;

    public void Start()
    {
        playerStartPosition = Player.transform.position;
        playerRigidBody = Player.GetComponent<Rigidbody2D>();
        levelTimer = FindObjectOfType<LevelTimer>();
    }

    public void ResetGame()
    {
        DeathParticle.transform.position = PlayerDeathPosition;
        DeathParticle.Play();

        Player.SetActive(false);

        StartCoroutine(RestartGame(0.8f));
    }

    public void HardResetGame()
    {
        Player.SetActive(false);
        levelTimer.TimeElapsed = 0;
        StartCoroutine(RestartGame(0f));
    }

    private IEnumerator RestartGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Player.transform.position = playerStartPosition;
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);

        playerRigidBody.velocity = Vector2.zero;
        playerRigidBody.gravityScale = 9;

        Player.SetActive(true);
    }
}
