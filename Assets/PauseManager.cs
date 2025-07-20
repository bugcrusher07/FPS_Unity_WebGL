using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public SFPSC_FPSCamera fpsController;
    public GameObject pauseMenu;
    private bool isPaused = false;
    private bool shouldRestoreCursor = false;


    [SerializeField] public TextMeshProUGUI score;
    private int enemiesKilled = 0;

    void Start()
    {
        Debug.Log("PauseManager Start - Initializing");
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Escape pressed - current pause state: " + isPaused);
            TogglePause();
        }

        if (shouldRestoreCursor)
        {
            RestoreCursorPosition();
        }
    }

    public void TogglePause()
    {
        Debug.Log("Toggling pause. New state: " + isPaused);
        isPaused = !isPaused;
        if (isPaused)
        {
            fpsController.SaveCurrentRotation();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {

            shouldRestoreCursor = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        Debug.Log("Game " + (isPaused ? "paused" : "unpaused"));
    }

    private void RestoreCursorPosition()
    {
        fpsController.RestoreSavedRotation();
        shouldRestoreCursor = false;

        InputSystem.ResetDevice(Mouse.current);
    }

    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);

    }
    public void EnemyKilled()
    {
        enemiesKilled++;
    }
    public void ChangeScore()
    {
        score.text = "Score-" + enemiesKilled.ToString();

    }


}
