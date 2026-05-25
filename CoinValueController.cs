using Unity.VisualScripting;
using UnityEngine;

public class CoinValueController : MonoBehaviour
{
    [Header("Coin Value")]
    [SerializeField] int value;
    [Header("Move Coin Settings")]
    float distanceToPlayer;
    PlayerMovement player;
    GameManager gameManager;
    [SerializeField] float coinRange;
    [SerializeField] float speed = 3f;

    [Header("Sound Settings")]
    [SerializeField] AudioClip coinSFX;
    [SerializeField][Range(0, 1)] float coinVolume;


    void Awake()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        MoveCoinsToPlayer();
    }


    void MoveCoinsToPlayer()
    {

        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < coinRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null)
        {
            AudioSource.PlayClipAtPoint(coinSFX, transform.position, coinVolume);
            gameManager.CoinsToAdd(value);
            Destroy(gameObject);
        }
    }
}
