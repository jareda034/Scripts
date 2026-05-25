using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIHandler : MonoBehaviour
{
    [Header("Player Health")]
    [SerializeField] Slider healthBar;
    [SerializeField] PlayerHealth health;
    [Header("Player Stamina")]
    [SerializeField] Slider staminaBar;
    [SerializeField] PlayerStamina stamina;
    [Header("Player Xp")]
    [SerializeField] Slider xpBar;
    [SerializeField] XpController xp;
    [Header("Potion UI")]
    [SerializeField] TextMeshProUGUI potionCountText;
    PotionsController potionsController;

    void Awake()
    {
        potionsController = FindFirstObjectByType<PotionsController>();
    }

    void Start()
    {
        healthBar.maxValue = health.GetPlayerHealth();
        staminaBar.maxValue = stamina.GetPlayerStamina();
        xpBar.maxValue = xp.GetMaxXp();
        xpBar.minValue = 0f;
    }

    public void UpdateSliders()
    {
        healthBar.maxValue = health.GetPlayerHealth();
        staminaBar.maxValue = stamina.GetPlayerStamina();
        xpBar.maxValue = xp.GetMaxXp();
    }


    void Update()
    {
        healthBar.value = health.GetPlayerHealth();
        staminaBar.value = stamina.GetPlayerStamina();
        xpBar.value = xp.GetPlayerXp();
        potionCountText.text = potionsController.GetHealthPotionCount().ToString();
    }
}
