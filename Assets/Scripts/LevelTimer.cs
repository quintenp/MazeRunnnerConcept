using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {
    public Text TimeText;
    public float TimeElapsed;

    void Update()
    {
        TimeElapsed +=  Time.deltaTime;
        TimeText.text = "Time Elapsed: " + string.Format("{0:f2}",TimeElapsed);
    }
}
