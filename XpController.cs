using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MonoBehaviours;


public class XpController : MonoBehaviour
{
    [Header("Xp Settings")]
    [SerializeField] float currentXp = 0f;
    [SerializeField] float xpTonextLevel = 100f;
    [SerializeField] int playerLevel = 0;
    [SerializeField] int skillPoints = 0;

    [Header("Stam & Health Settings")]
    [SerializeField] float levelUpHealth = 6f;
    [SerializeField] float levelUpStamina = 15f;
    [SerializeField] float LevelUpDamage = 2f;

    PlayerStamina playerStamina;
    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    SpriteRenderer sr;
    Animator animator;
    [SerializeField] GameObject skillTree;
    SetPlayerSprite setPlayer;
    GameUIHandler gameUI;
    [Header("Player Sound Settings")]
    [SerializeField] AudioClip playerLevelUpSFX;
    [SerializeField][Range(0, 1)] float levelUpVolume;


    float loadSkillTreeTime = 0.5f;

    void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        playerAttack = FindFirstObjectByType<PlayerAttack>();
        playerStamina = FindFirstObjectByType<PlayerStamina>();
        setPlayer = FindFirstObjectByType<SetPlayerSprite>();
        gameUI = FindAnyObjectByType<GameUIHandler>();
        skillTree.SetActive(false);
    }

    public void GainXp(float xpAmount)
    {
        currentXp += xpAmount;
        Debug.Log("Gained " + xpAmount + " XP");
        if (currentXp >= xpTonextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        playerLevel++;
        currentXp = 0f;
        xpTonextLevel *= 1.5f;
        skillPoints++;
        LevelUpEffects();
        gameUI.UpdateSliders();
        AudioSource.PlayClipAtPoint(playerLevelUpSFX, transform.position, levelUpVolume);
    
    }



    void LevelUpEffects()
    {
        if (playerLevel == 1)
        {
            playerHealth.IncreaseMaxHealth(levelUpHealth);
            playerStamina.IncreaseStamina(levelUpStamina);
            playerAttack.IncreaseDamage(LevelUpDamage);
            if (skillPoints > 0)
            {
                Invoke(nameof(LoadSkillTree), loadSkillTreeTime);
                gameUI.UpdateSliders();
            }

        }
        if (playerLevel == 2)
        {
            playerHealth.IncreaseMaxHealth(levelUpHealth);
            playerStamina.IncreaseStamina(levelUpStamina);
            if (skillPoints > 0)
            {
                Invoke(nameof(LoadSkillTree), loadSkillTreeTime);
                setPlayer.Set(0);
                gameUI.UpdateSliders();

            }
        }

        if (playerLevel == 3)
        {
            playerHealth.IncreaseMaxHealth(levelUpHealth);
            playerStamina.IncreaseStamina(levelUpStamina);
            if (skillPoints > 0)
            {
                Invoke(nameof(LoadSkillTree), loadSkillTreeTime);
                gameUI.UpdateSliders();
            }

        }
        if (playerLevel == 4)
        {
            playerHealth.IncreaseMaxHealth(levelUpHealth);
            playerStamina.IncreaseStamina(levelUpStamina);
            if (skillPoints > 0)
            {
                Invoke(nameof(LoadSkillTree), loadSkillTreeTime);
                setPlayer.Set(1);
                gameUI.UpdateSliders();
            }

        }
        if (playerLevel == 5)
        {
            playerHealth.IncreaseMaxHealth(levelUpHealth);
            playerStamina.IncreaseStamina(levelUpStamina);
            if (skillPoints > 0)
            {
                Invoke(nameof(LoadSkillTree), loadSkillTreeTime);
                gameUI.UpdateSliders();
            }

        }

    }

    void LoadSkillTree()
    {
        skillTree.SetActive(true);
        Time.timeScale = 0f;
    }


    public void SkillChoosen(int skillCost)
    {
        skillPoints -= skillCost;
        skillTree.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PointsSaved()
    {
        skillTree.SetActive(false);
        Time.timeScale = 1f;
    }

    public float GetPlayerXp()
    {
        return currentXp;
    }

    public float GetMaxXp()
    {
        return xpTonextLevel;
    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }





}
