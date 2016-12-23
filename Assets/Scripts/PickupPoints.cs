using UnityEngine;

public class PickupPoints : MonoBehaviour {

    public int ScoreToGive;

    private AudioSource coinSound;

    private ScoreManager scoreManager;

	void Start () {
        scoreManager = FindObjectOfType<ScoreManager>();
        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>(); 
    }
	
	void Update () {
        
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
            
            scoreManager.AddToScore(ScoreToGive);
            gameObject.SetActive(false);
        }       
    }
}
