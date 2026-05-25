
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    [Header("Attack Settings")]
    [SerializeField] float attackDamage, maxAttackDamage = 5f;
    [SerializeField] float KnockBackForce = 1f;
    [SerializeField] float attackCooldown = 3f;
    [SerializeField] float attackCost = 10f;
    [Header("Sound Settings")]
    [SerializeField] AudioClip playerAttackSFX;
    [SerializeField][Range(0, 1)] float attackVolume;

    bool canAttack = true;
    public bool isAttacking = false;
    SkillsController skillsController;
    EnemyHealth enemyHealth;


    [Header("Stamina Settings")]
    PlayerStamina playerStamina;

    [Header("Attack Range Settings")]
    [SerializeField] GameObject playerAttackPoint;
    [SerializeField] float radius;
    [SerializeField] LayerMask enemies;

    [Header("Effecient Swings Settings")]
    [SerializeField] int attackCount;
    bool canEffecintAttack = false;

    [Header("CounterSlice Settings")]
    bool counterSliceUnlocked = false;
    bool counterReady = false;
    
    [Header("Double Slice Settings")]
    [SerializeField] float specialCost = 18f;
    [SerializeField] float specialDamage = 15;
    [SerializeField] float specialCoolDown = 12f;
    bool canSpecial = true;
    bool specialUnlocked = false;

    [Header("Crippling Strike Settings")]
    bool cripplingStrikeActive = false;
    [SerializeField] float decraseEnemyDamage = 3.5f;

    [Header("Deadly Poison Settings")]
    [SerializeField] int poisonDamage = 1;
    [SerializeField] int poisonTime = 5;
    bool isPoisoned = false;
    bool deadlyPoisonUnlocked = false;

    [Header("Holy Warth Settings")]
    [SerializeField] float holyDamage = 3.5f;
    bool holyWarthUnlocked = false; 

    [Header("Soul Crush Settings")]
    [SerializeField] float crushDamage = 30f;
    [SerializeField] float crushCooldown = 40f;
    [SerializeField] GameObject soulCrushPoint;
    [SerializeField] float crushRadius;
    bool canCrush = true;
    bool soulCrushUnlocked = false;
    public bool isCrushing;

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerStamina = GetComponent<PlayerStamina>();
        skillsController = FindFirstObjectByType<SkillsController>();
        attackDamage = maxAttackDamage;
    }

    public void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(playerAttackPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameObject in enemy)
        {
            if (enemyGameObject.GetComponent<EnemyHealth>().GetEnemyHealth() <= 0)
            {
                return;
            }
            enemyGameObject.GetComponent<EnemyHealth>().EnemyTakeDamage(attackDamage);
            if (cripplingStrikeActive)
            {
                enemyGameObject.GetComponent<EnemyAttack>().WeakenEnemy(decraseEnemyDamage);
            }
            if (deadlyPoisonUnlocked)
            {
                enemyGameObject.GetComponent<EnemyHealth>().PoisonEnemy(poisonDamage , poisonTime);
            }
            if (holyWarthUnlocked)
            {
                enemyGameObject.GetComponent<EnemyHealth>().TakeHolyDamage(holyDamage);
            }
            Vector2 KnockBackDirection = (enemyGameObject.transform.position - transform.position).normalized;
            enemyGameObject.GetComponent<Rigidbody2D>().AddForce(KnockBackDirection * KnockBackForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(playerAttackPoint.transform.position, radius);
        Gizmos.DrawWireSphere(soulCrushPoint.transform.position, crushRadius);
    }


    void OnAttack(InputValue value)
    {
        if (playerStamina.GetPlayerStamina() < attackCost)
        {
            Debug.Log("Out Of Stamina");
            return;
        }
        if (value.isPressed && canAttack)
        {
            animator.SetBool("isAttacking", true);
            AudioSource.PlayClipAtPoint(playerAttackSFX, transform.position, attackVolume);
            canAttack = false;
            isAttacking = true;
            Invoke(nameof(ResetAttack), attackCooldown);
            if (counterSliceUnlocked)
            {
              ResetCounterSlice();
            }
            if (canEffecintAttack)
            {
                attackCount++;
            }
            if (attackCount == 3)
            {
                playerStamina.AttackCost(0);
                attackCount = 0;
            }
            else
            {
                playerStamina.AttackCost(attackCost);
            }

        }
    }

    public void StopAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;

    }

    void ResetAttack()
    {
        canAttack = true;
    }

    public void AddAttackDamage(float addedDamage)
    {
        maxAttackDamage += addedDamage;
        attackDamage = maxAttackDamage;
    }

    public void EffecientAttack()
    {
        canEffecintAttack = true;
    }

    void OnSpecial(InputValue value)
    {
        if (playerStamina.GetPlayerStamina() < specialCost)
        {
            return;
        }
        if (value.isPressed && canSpecial && specialUnlocked)
        {
            animator.SetBool("isSpecialing", true);
            AudioSource.PlayClipAtPoint(playerAttackSFX, transform.position, attackVolume);
            canSpecial = false;
            isAttacking = true;
            Invoke(nameof(ResetSpecial), specialCoolDown);
            playerStamina.AttackCost(specialCost);
        }
    }

    void ResetSpecial()
    {
        canSpecial = true;
    }
    public void EndSpecial()
    {
        animator.SetBool("isSpecialing", false);
        isAttacking = false;
    }

    public void DoubleSwing()
    {
        specialUnlocked = true;
    }

    public void Special()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(playerAttackPoint.transform.position, radius, enemies);
        foreach (Collider2D enemyGameObject in enemy)
        {
            if (enemyGameObject.GetComponent<EnemyHealth>().GetEnemyHealth() <= 0)
            {
                return;
            }
            enemyGameObject.GetComponent<EnemyHealth>().EnemyTakeDamage(specialDamage);
            Vector2 KnockBackDirection = (enemyGameObject.transform.position - transform.position).normalized;
            enemyGameObject.GetComponent<Rigidbody2D>().AddForce(KnockBackDirection * KnockBackForce, ForceMode2D.Impulse);
        }
    }

     public void CripplingStrike()
    {
        cripplingStrikeActive = true;
    }

    public void UnlockCounterSlice()
    {
        counterSliceUnlocked = true;
    }

    public void CounterSlice(float addDamage)
    {
        if (counterReady)
        {
            return;
        }
        if (counterSliceUnlocked)
        {
            attackDamage += addDamage;
            counterReady = true;
        }
    }

    void ResetCounterSlice()
    {
        attackDamage = maxAttackDamage;
        counterReady = false;
        
    }

    public void UnlockDeadlyPoison()
    {
        deadlyPoisonUnlocked = true;
    }


    public void UnlockHolyWarth()
    {
        holyWarthUnlocked = true;
    }

    public void UnlockSoulCrush()
    {
        soulCrushUnlocked = true;
    }

    void OnSoulCrush(InputValue value)
    {
        if (value.isPressed && canCrush && soulCrushUnlocked)
        {
            animator.SetBool("isCrushing", true);
            canCrush = false;
            isAttacking = true;
            Invoke(nameof(ResetCrush), crushCooldown);
        }
    }

    void ResetCrush()
    {
        canCrush = true;
    }

    public void SoulCrush()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(soulCrushPoint.transform.position, crushRadius, enemies);
        foreach (Collider2D enemyGameObject in enemy)
        {
            if (enemyGameObject.GetComponent<EnemyHealth>().GetEnemyHealth() <= 0)
            {
                return;
            }
            enemyGameObject.GetComponent<EnemyHealth>().EnemyTakeDamage(crushDamage);
        }
    }
    
    public void EndCrush()
    {
        animator.SetBool("isCrushing", false);
        isAttacking = false;
        isCrushing = false;
    }

    public void IncreaseDamage(float addedDamage)
    {
        maxAttackDamage += addedDamage;
        attackDamage = maxAttackDamage;
    }


}
