using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerCoins;
    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] GameObject pauseScreen;

    PlayerHealth playerHealth;
    PauseMenuController pauseMenu;

    [Header("UI Sound Settings")]
    [SerializeField] AudioClip uiSfx;
    [SerializeField][Range(0, 1)] float uiVolume;



    void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        pauseMenu = FindFirstObjectByType<PauseMenuController>();
        pauseScreen.SetActive(false);
        coinText.text = playerCoins.ToString();
        GamePersist();
    }

    void GamePersist()
    {
        int numberOfGameSessions = FindObjectsByType<GameManager>(FindObjectsSortMode.None).Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CoinsToAdd(int value)
    {
        playerCoins += value;
        coinText.text = playerCoins.ToString();
    }

    void PlayerDied()
    {
        if (playerHealth.GetPlayerHealth() <= 0)
        {
            Invoke(nameof(LoadGameOver), 1f);
        }
    }

    void LoadGameOver()
    {

        Time.timeScale = 0f;
        SceneManager.LoadScene("Game Over");
    }

    public void ResetGame()
    {
        playerCoins = 0;
        coinText.text = playerCoins.ToString();
    }

    public void PauseGame()
    {
        if (pauseMenu.MenuBeingUsed())
        {
            return;
        }
        if (Time.timeScale == 1f)
        {
            AudioSource.PlayClipAtPoint(uiSfx, transform.position, uiVolume);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else if (Time.timeScale == 0f)
        {
            AudioSource.PlayClipAtPoint(uiSfx, transform.position, uiVolume);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    void Update()
    {
        PlayerDied();
    }

    public int GetPlayerCoins()
    {
        return playerCoins;
    }
}
