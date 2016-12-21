using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string MainMenuLevel;
    public GameObject ThePauseMenu;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().HardResetGame();
        ThePauseMenu.SetActive(false);
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuLevel);
        ThePauseMenu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ThePauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ThePauseMenu.SetActive(false);
    }
}
