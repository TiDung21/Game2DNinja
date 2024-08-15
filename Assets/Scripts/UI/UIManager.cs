using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Game Pause")]
    [SerializeField] private GameObject gamePauseScreen;

    [Header("Game Win")]
    [SerializeField] private GameObject gameWinScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        gamePauseScreen.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gamePauseScreen.activeInHierarchy);
        }
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
        Time.timeScale = 0f;
    }
    public void PauseGame(bool _status)
    {
        gamePauseScreen.SetActive(_status);
        Time.timeScale = _status ? 0 : 1f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gamePauseScreen.SetActive(false);
    }
    public void WinLevel()
    {
        Time.timeScale = 0f;
        gameWinScreen.SetActive(true);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
