using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
  [SerializeField] GameObject GameOver;
  GameManager gameManager;
  PlayerMovement player;

  void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        player = FindFirstObjectByType<PlayerMovement>();
        GameOver.SetActive(true);
        Destroy(gameManager.gameObject);
       Destroy(player.gameObject);
    }


  public void RestartGame()
    {
       SceneManager.LoadScene("GrassLand");
       GameOver.SetActive(false);
       Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

}
