using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerMovement player;
    [Header("Attack Settings")]
    [SerializeField] float attackDamage = 5f;
    [SerializeField] float maxAttackDamage;
    [SerializeField] float KnockBackForce = 1f;
    [SerializeField] float attackCooldown = 3f;
    [SerializeField] GameObject enemyAttackPoint;
    public bool seenPlayer;
    bool canAttack = true;
    public bool isAttacking = false;
    [Header("Attack Range")]
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask playerLayer;
    [Header("Attack Sound Settings")]
    [SerializeField] AudioClip enemyAttackSFX;
    [SerializeField][Range(0, 1)] float enemyAttackVolume;
    Animator animator;
    EnemyHealth enemyHealth;

    [Header("Skill Settings")]
    [SerializeField] float weakDuration = 5f;
    public bool enenmyWeakened = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = FindFirstObjectByType<PlayerMovement>();
        seenPlayer = false;
    }



    void Update()
    {

    }

    public void Attack()
    {
        if (enemyHealth.GetEnemyHealth() <= 0 && enemyHealth.enemyTakenDamage)
        {
            return;
        }

        Collider2D playerGameObject = Physics2D.OverlapCircle(enemyAttackPoint.transform.position, attackRange, playerLayer);
        if (playerGameObject != null)
        {
            playerGameObject.GetComponent<PlayerHealth>().PlayerTakeDamage(attackDamage);
            Vector2 KnockBackDirection = (playerGameObject.transform.position - transform.position).normalized;
            playerGameObject.GetComponent<Rigidbody2D>().AddForce(KnockBackDirection * KnockBackForce, ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (enemyHealth.GetEnemyHealth() <= 0 && enemyHealth.enemyTakenDamage)
        {
            return;
        }
        if (player != null && canAttack && seenPlayer)
        {
            isAttacking = true;
            canAttack = false;
            AudioSource.PlayClipAtPoint(enemyAttackSFX, transform.position, enemyAttackVolume);
            animator.SetBool("isAttacking", true);
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    void ResetAttack()
    {
        canAttack = true;
    }

    public void EndAttack()
    {
        animator.SetBool("isAttacking", false);
        isAttacking = false;
    }

    public void WeakenEnemy(float damageDecrease)
    {
        if (weakDuration == 5f && enenmyWeakened == false)
        {
            enenmyWeakened = true;
            attackDamage -= damageDecrease;
            Invoke(nameof(ResetDamage), weakDuration);
        }
    }

    void ResetDamage()
    {
        attackDamage = maxAttackDamage;
        weakDuration = 5f;
        enenmyWeakened = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(enemyAttackPoint.transform.position, attackRange);

    }

}
