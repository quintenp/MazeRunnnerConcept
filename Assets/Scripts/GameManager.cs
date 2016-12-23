using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public ParticleSystem DeathParticle;
    public Vector3 PlayerDeathPosition;
    public ObjectPooler DeathParticlePool;

    private LevelTimer levelTimer;
    private Vector3 playerStartPosition;
    private Rigidbody2D playerRigidBody;
    private ScoreManager scoreManager;

    public void Start()
    {
        playerStartPosition = Player.transform.position;
        playerRigidBody = Player.GetComponent<Rigidbody2D>();
        levelTimer = FindObjectOfType<LevelTimer>();
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void ResetGame()
    {
        var deathEffect = DeathParticlePool.GetPooledObject().GetComponent<ParticleSystem>();
        deathEffect.gameObject.SetActive(true);
        deathEffect.transform.position = PlayerDeathPosition;
        deathEffect.Play();

        if (!deathEffect.isEmitting)
        {
            deathEffect.gameObject.SetActive(false);
        }

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

        var coins = Resources.FindObjectsOfTypeAll<PickupPoints>();

        foreach (var coin in coins)
        {
            coin.gameObject.SetActive(true);
        }

        scoreManager.ScoreCount = 0;

        Player.transform.position = playerStartPosition;
        Player.transform.rotation = Quaternion.Euler(0, 0, 0);

        playerRigidBody.velocity = Vector2.zero;
        playerRigidBody.gravityScale = 9;

        Player.SetActive(true);
    }
}
