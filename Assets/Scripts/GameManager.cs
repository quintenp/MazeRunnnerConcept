using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public ParticleSystem DeathParticle;
    public Vector3 PlayerDeathPosition;

    public void ResetGame()
    {
        DeathParticle.transform.position = PlayerDeathPosition;
        DeathParticle.Play();

        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.5f);

        Player.SetActive(true);
    }
}
