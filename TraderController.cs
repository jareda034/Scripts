
using Unity.VisualScripting;
using UnityEngine;

public class TraderController : MonoBehaviour
{
    [SerializeField] GameObject traderUI;
    PlayerMovement player;
    GameManager gameManager;
    PotionsController potionsController;
    [Header("Trader Shop Settings")]
    [SerializeField] int healthPotionPrice = 225;
    [Header("Sound Effects")]
    [SerializeField] AudioClip buySoundEfx;
    [SerializeField][Range(0, 1)] float buyVolume;
    [Header("UI Sound Settings")]
    [SerializeField] AudioClip uiSfx;
    [SerializeField][Range(0, 1)] float uiVolume;


    void Awake()
    {
        traderUI.SetActive(false);
        gameManager = FindFirstObjectByType<GameManager>();
        potionsController = FindFirstObjectByType<PotionsController>();
        player = FindFirstObjectByType<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            traderUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void BuyHealthPotion()
    {
        if (gameManager.GetPlayerCoins() >= healthPotionPrice)
        {
            gameManager.CoinsToAdd(-healthPotionPrice);
            Debug.Log("Health Potion Bought");
            potionsController.AddPotion();
            AudioSource.PlayClipAtPoint(buySoundEfx, transform.position, buyVolume);
        }
    }

    public void ExitTrade()
    {
        traderUI.SetActive(false);
        Time.timeScale = 1f;
        PlayAudio(uiSfx, uiVolume);
    }

    void PlayAudio(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }
}
