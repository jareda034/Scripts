using System;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] float playerHealth = 25f;
    [SerializeField] float deathTime = 1.5f;
    [SerializeField] float maxPlayerHealth = 25f;
    [SerializeField] float playerArmour = 0;

    [Header("BloodThrist Skill Settings")]
    [SerializeField] float healthRegenRate = 1.5f;
    float regenTime = 1f;
    public bool skillBloodThristActive = false;

    [Header("Bulwark Settings")]
    bool bulkwarkUnlocked = false;
    [SerializeField] int hitCount;
    [SerializeField] int maxHits = 6;
    bool bulwarkUsed = false;
    bool bulwarkActive = false;

    [Header("Hearty Settings")]
    [SerializeField] float heartyHealthIncrease = 50f;

    [Header("ArmourPlates Settings")]
    [SerializeField] float Armour = 3f;

    [Header("Perception Settings")]
    [SerializeField] float dodgeTimer = 15f;
    public bool dodgeReady = false;
    public bool dodgeUsed = true;
    bool perceptionUnlocked = false;

    [Header("Sinister Ward Settings")]
    [SerializeField] float wardArmour = 2.5f;
    bool wardActive = false;
    bool sinisterWardUnlocked = false;


    [Header("CounterSlice Damage")]
    [SerializeField] float counterDamage = 4f;

    [Header("Sound Settings")]
    [SerializeField] AudioClip playerHurtSFX;
    [SerializeField][Range(0, 1)] float hurtVolume;
    [SerializeField] AudioClip playerDeathSFX;
    [SerializeField][Range(0, 1)] float deathVolume;

    float iFrames = 0.5f;
    bool hitIFrames = false;
    public bool playerTakenDamage = false;
    Animator animator;
    PlayerMovement playerMovement;
    PlayerAttack playerAttack;
    SkillsController skillsController;
    GameUIHandler gameUI;


    void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = maxPlayerHealth;
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        skillsController = FindFirstObjectByType<SkillsController>();
        gameUI = FindAnyObjectByType<GameUIHandler>();
    }

    void Update()
    {
        BloodThrist();
        Bulwark();
        ResetBulwark();
        Perception();
        ResetPerception();
    }


    public void PlayerTakeDamage(float damage)
    {
        if (hitIFrames || playerMovement.isInvincible)
        {
            return;
        }
        if (dodgeReady)
        {
            dodgeUsed = true;
            return;
        }
        if (bulwarkActive)
        {
            bulwarkUsed = true;
            return;
        }

        damage -= playerArmour;
        playerHealth -= damage;
        animator.SetBool("isHurt", true);
        animator.SetFloat("Speed", 0f);
        hitIFrames = true;
        playerTakenDamage = true;

        if (bulkwarkUnlocked)
        {
            hitCount++;
        }

        playerAttack.CounterSlice(counterDamage);
        RemoveWardArmour();
        Invoke(nameof(ResetIFrames), iFrames);

        if (playerHealth >= 0.1)
        {
            AudioSource.PlayClipAtPoint(playerHurtSFX, transform.position, hurtVolume);
        }
        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void ResetIFrames()
    {
        hitIFrames = false;
        playerTakenDamage = false;
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, transform.position, deathVolume);
        Destroy(gameObject, deathTime);
        animator.SetBool("isDead", true);

    }

    public void EndHurt()
    {
        animator.SetBool("isHurt", false);
    }

    public void IncreaseMaxHealth(float healthIncrease)
    {
        maxPlayerHealth += healthIncrease;
        playerHealth = maxPlayerHealth;
    }
    
    public void RestoreHealth(float healing)
    {
        playerHealth += healing;
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }
    public float GetMaxPlayerHealth()
    {
        return maxPlayerHealth;
    }


    void BloodThrist()
    {

        if (skillBloodThristActive && skillsController.bloodThristUnlocked)
        {
            playerHealth += healthRegenRate * Time.deltaTime;
            if (playerHealth > maxPlayerHealth)
            {
                playerHealth = maxPlayerHealth;
            }
            Invoke(nameof(ResetBloodThirst), regenTime);
        }
    }

    void ResetBloodThirst()
    {
        skillBloodThristActive = false;
    }

    public void UnlockBulwark()
    {
        bulkwarkUnlocked = true;
    }

    void Bulwark()
    {
        if (bulkwarkUnlocked && hitCount == maxHits)
        {
            bulwarkActive = true;
        }
    }

    void ResetBulwark()
    {
        if (bulwarkUsed)
        {
            bulwarkActive = false;
            hitCount = 0;
            bulwarkUsed = false;
        }
    }

    public void UnlockHearty()
    {
        maxPlayerHealth += heartyHealthIncrease;
        playerHealth = maxPlayerHealth;
        gameUI.UpdateSliders();
    }

    public void UnlockArmourPlates()
    {
        playerArmour += Armour;
    }

    public void UnlockPerception()
    {
        perceptionUnlocked = true;
    }

    void Perception()
    {
        if (perceptionUnlocked)
        {
            if (dodgeTimer > 0)
            {
                dodgeTimer -= Time.deltaTime;
            }
            else if (dodgeTimer <= 0)
            {
                dodgeTimer = 0;
                Debug.Log("Dodge Ready");
                dodgeReady = true;
            }
        }
    }

    void ResetPerception()
    {
        if (dodgeUsed)
        {
            dodgeReady = false;
            dodgeTimer = 15;
            dodgeUsed = false;
        }
    }

    public void UnlockSinisterWard()
    {
        sinisterWardUnlocked = true;
    }

    public void SinisterWard()
    {
        if (sinisterWardUnlocked)
        {
            if (!wardActive)
            {
                playerArmour += wardArmour;
                wardActive = true;
            }
        }
    }

    void RemoveWardArmour()
    {
        if (wardActive)
        {
            playerArmour -= wardArmour;
            wardActive = false;
        }
    }

}
