using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text ScoreText;
    public Text HighScoreText;

    public float ScoreCount;
    public float HighScoreCount;
    public float PointsPerSeconds;

    public bool ScoreIncreasing;
    public bool ShouldDouble;

	void Start () {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            HighScoreCount = PlayerPrefs.GetFloat("HighScore");
        }     
    }

    void Update()
    {
        if (ScoreIncreasing)
        {
            if (ShouldDouble)
            {
                ScoreCount += (PointsPerSeconds * 2) * Time.deltaTime;
            }

            ScoreCount += PointsPerSeconds * Time.deltaTime;
        }

        if (ScoreCount > HighScoreCount)
        {
            HighScoreCount = ScoreCount;
            PlayerPrefs.SetFloat("HighScore", HighScoreCount);
        }

        ScoreText.text = "Score: " + Mathf.Round(ScoreCount);
        HighScoreText.text = "High Score: " + Mathf.Round(HighScoreCount);
    }

    public void AddToScore(int sccoreIncrement)
    {
        if (ShouldDouble)
        {
            sccoreIncrement = sccoreIncrement * 2;
        }

        ScoreCount += sccoreIncrement;
    }
}
