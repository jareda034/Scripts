using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [Header("Health Settings")]
    [SerializeField] float health = 15f;
    [SerializeField] float deathTime = 1.5f;
    [SerializeField] float xpValue = 20f;
    public bool enemyTakenDamage = false;
    bool isDead = false;
    bool hitIframes = false;
    [Header("Sound Settings")]
    [SerializeField] AudioClip enemyHitSound;
    [SerializeField][Range(0, 1)] float hitSoundVolume;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume;

    [Header("Loot Drop Settings")]
    [SerializeField] GameObject[] lootDrop;
    [SerializeField] Transform[] dropPoints;
    [SerializeField] float dropTime = 0.5f;


    [Header("Deadly Poison Settings")]
    public bool isPoisoned = false;

    [Header("Undead & Demon Settings")]
    [SerializeField] bool isDemon;
    [SerializeField] bool isUndead;

    CircleCollider2D enemyCollider;
    Animator animator;
    XpController xpController;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;

    void Awake()
    {
        animator = GetComponent<Animator>();
        xpController = FindFirstObjectByType<XpController>();
        enemyCollider = GetComponent<CircleCollider2D>();
        playerHealth = FindAnyObjectByType<PlayerHealth>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }


    void Update()
    {

    }

    public void EnemyTakeDamage(float damage)
    {
        if (hitIframes)
        {
            return;
        }
        health -= damage;
        enemyTakenDamage = true;
        animator.SetBool("isHurt", true);
        hitIframes = true;
        Invoke(nameof(ResetHitIframes), 0.2f);
        if (health >= 5)
        {
            AudioSource.PlayClipAtPoint(enemyHitSound, transform.position, hitSoundVolume);
        }
        if (health <= 0)
        {
            EnemyDie();
        }
    }

    void TakePoisonDamage(int damage)
    {
        health -= damage;
        if (isDead)
        {
            return;
        }
        if (health <= 0)
        {
            EnemyDie();
        }
    }

    public void EnemyDie()
    {
        isDead = true;
        AudioSource.PlayClipAtPoint(deathSound, transform.position, deathSoundVolume);
        Destroy(gameObject, deathTime);
        animator.SetBool("isDead", true);
        enemyCollider.enabled = false;
        xpController.GainXp(xpValue);
        playerHealth.skillBloodThristActive = true;
        playerMovement.Frenzy();
        playerHealth.SinisterWard();
        Invoke(nameof(LootDrop), dropTime);
    }

    void LootDrop()
    {
         for (int i = 0; i < lootDrop.Length; i++)
        {
            Instantiate(lootDrop[i], dropPoints[i].position, Quaternion.identity);
        }
    }



    void ResetHitIframes()
    {
        hitIframes = false;
    }

    public void EndHurt()
    {
        animator.SetBool("isHurt", false);
        enemyTakenDamage = false;
    }

    public float GetEnemyHealth()
    {
        return health;
    }

    public void PoisonEnemy(int poisonDamage, int duration)
    {
        if (!isPoisoned)
        {
            StartCoroutine(PoisonCoroutine(poisonDamage, duration));
        }
    }

    IEnumerator PoisonCoroutine(int damage, int seconds)
    {
        isPoisoned = true;
        for (int i = 0; i < seconds; i++)
        {
            TakePoisonDamage(damage);
            yield return new WaitForSeconds(1.0f);
        }
        isPoisoned = false;
    }

    public void TakeHolyDamage(float damage)
    {
        if (isDemon || isUndead)
        {
           health -= damage;
        }
        if (isDead)
        {
            return;
        }
        if (health <= 0)
        {
            EnemyDie();
        }
    }


}
