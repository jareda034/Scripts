using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameOver : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
