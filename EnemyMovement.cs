

using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //Componentes
    PlayerMovement player;
    EnemyHealth enemyHealth;
    EnemyAttack enemyAttack;
    [SerializeField] GameObject attackPoint;
    [SerializeField] float offsetDistance = 1f;
    Animator animator;
    [Header("Movement")]
    [SerializeField] float speed = 3f;
    [SerializeField] float enemyDectionRange = 5f;
    [Header("Movement Sound Settings")]
    [SerializeField] AudioClip enemyMoveSFX;
    [SerializeField][Range(0, 1)] float enemyMoveVolume;
    [SerializeField] float moveSoundDelay = 0.5f;
    float nextSoundTime = 0f;
    Vector2 lastDirection;

    float distanceToPlayer;


    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        animator = GetComponent<Animator>();
        enemyAttack = GetComponent<EnemyAttack>();
        player = FindFirstObjectByType<PlayerMovement>();
    }


    void Update()
    {
        MoveEnemyToPlayer();
    }

    void MoveEnemyToPlayer()
    {
        if (enemyHealth.GetEnemyHealth() <= 0 || enemyHealth.enemyTakenDamage || enemyAttack.isAttacking || player == null)
        {
            animator.SetBool("isMoving", false);
            return;
        }
        distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < enemyDectionRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            enemyAttack.seenPlayer = true;
            if (Time.time >= nextSoundTime)
            {
                AudioSource.PlayClipAtPoint(enemyMoveSFX, transform.position, enemyMoveVolume);
                nextSoundTime = Time.time + moveSoundDelay;
            }

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        animator.SetFloat("Horizontal", player.transform.position.x - transform.position.x);
        animator.SetFloat("Vertical", player.transform.position.y - transform.position.y);

        if (transform.position != Vector3.zero)
        {
            lastDirection = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            SnapAttackPoint(lastDirection);
        }
        animator.SetFloat("LastHorizontal", lastDirection.x);
        animator.SetFloat("LastVertical", lastDirection.y);
    }

    void SnapAttackPoint(Vector2 direction)
    {
        Vector2 snapPos = direction.normalized * offsetDistance;
        attackPoint.transform.localPosition = new Vector3(snapPos.x, snapPos.y, 0f);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDectionRange);
    }
}
