using UnityEngine;

public class ChestController : MonoBehaviour
{
    [Header("Chest Settings")]
    [SerializeField] GameObject[] chestContents;
    [SerializeField] Transform[] dropPoints;
    [SerializeField] float dropDelay = 0.5f;
    PlayerMovement player;
    bool isOpened = false;

    [Header("Sound Settings")]
    [SerializeField] AudioClip chestSFX;
    [SerializeField][Range(0, 1)] float chestVolume;

    
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<PlayerMovement>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() == player && !isOpened)
        {
            animator.SetBool("isOpen", true);
            AudioSource.PlayClipAtPoint(chestSFX, transform.position, chestVolume);
            isOpened = true;
            Invoke(nameof(dropItems), dropDelay);
        }

    }

    void dropItems()
    {
        for (int i = 0; i < chestContents.Length; i++)
        {
            Instantiate(chestContents[i], dropPoints[i].position, Quaternion.identity);
        }
    }
}
