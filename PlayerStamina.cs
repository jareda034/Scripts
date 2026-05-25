using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
 [Header("Stamina Settings")]
    [SerializeField] public float playerStamina, maxStamina = 100f;
    [SerializeField] float StaminaRegenRate = 2.5f;
    [Header("Active Recovery Settings")]
    [SerializeField] float addRegen = 2.5f;
     [Header("Athlete Settings")]
    [SerializeField] float skillStam = 50f;

    PlayerAttack playerAttack;
    PlayerMovement playerMovement;
    GameUIHandler gameUI;



    void Awake()
    {
        playerStamina = maxStamina;
        gameUI = FindAnyObjectByType<GameUIHandler>();
    }

    void Update()
    {
        StaminaRegen();
    }

    void StaminaRegen()
    {
        if (playerStamina < maxStamina)
        {
            playerStamina += StaminaRegenRate * Time.deltaTime;
            if (playerStamina > maxStamina)
            {
                playerStamina = maxStamina;
            }
        }
    }

     public void DashCost(float cost)
    {
        playerStamina -= cost;
    }

    public void AttackCost(float cost)
    {
        playerStamina -= cost;
    }
    
    public void IncreaseStamina(float addStam)
    {
        maxStamina += addStam;
        playerStamina = maxStamina;
    }

    public void UnlockActiveRecovery()
    {
        StaminaRegenRate += addRegen;
    }

    public void UnlockAthlete()
    {
        maxStamina += skillStam;
        playerStamina = maxStamina;
        gameUI.UpdateSliders();
    }

    public float GetPlayerStamina()
    {
        return playerStamina;
    }

    
}
