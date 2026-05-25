using UnityEngine;

public class PotionsController : MonoBehaviour
{
   [Header("Health Potion Settings")]
   [SerializeField] int healthRestoreAmount = 50;
   [SerializeField] int healthPotionCount;

   [Header("Sound Effects")]
   [SerializeField] AudioClip usePotionSfx;
   [SerializeField] [Range(0,1)] float usePotionVolume;
   PlayerHealth playerHealth;

   void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
    }

    public void AddPotion()
    {
        healthPotionCount++;
    }

    public int GetHealthPotionCount()
    {
        return healthPotionCount;
    }

    public void UseHealthPotion()
    {
        playerHealth.RestoreHealth(healthRestoreAmount);
        healthPotionCount--;
        AudioSource.PlayClipAtPoint(usePotionSfx, transform.position, usePotionVolume);
    }
}
