
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 lastDirection;
    Animator animator;

    [Header("Movement Settings")]
    [SerializeField] float moveSpeed, maxMoveSpeed = 5f;
    [SerializeField] float dashSpeed = 15f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] float dashCost = 15f;
    bool isDashing = false;
    public bool isInvincible = false;

    [Header("Attack Point Settings")]
    [SerializeField] GameObject attackPoint;
    [SerializeField] float offsetDistance = 1f;

    [Header("Sound Settings")]
    [SerializeField] AudioClip playerMoveSFX;
    [SerializeField][Range(0, 1)] float moveVolume;
    [SerializeField] float moveSoundDelay = 0.5f;
    float nextSoundTime = 0f;
    PlayerHealth playerHealth;
    PlayerAttack playerAttack;
    PlayerStamina playerStamina;
    SkillsController skillsController;
    BoxCollider2D playerCollider;
    GameManager gameManager;
    PotionsController potionsController;

    [Header("Frenzy Settings")]
    [SerializeField] float frenzyTime = 3f;
    [SerializeField] float speedIncrease = 1f;
    public bool frenzyActive = false;
    bool frenzyUnlocked = false;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GetComponent<PlayerAttack>();
        playerStamina = GetComponent<PlayerStamina>();
        skillsController = FindFirstObjectByType<SkillsController>();
        playerCollider = GetComponent<BoxCollider2D>();
        gameManager = FindFirstObjectByType<GameManager>();
        potionsController = FindFirstObjectByType<PotionsController>();
        moveSpeed = maxMoveSpeed;
    }


    void Update()
    {
        if (isDashing || playerAttack.isAttacking)
        {
            return;
        }
        MovePlayer();
    }

    void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            gameManager.PauseGame();
        }
    }

    void OnHeal(InputValue value)
    {
        if (value.isPressed)
        {
            if (potionsController.GetHealthPotionCount() > 0 && playerHealth.GetPlayerHealth() < playerHealth.GetMaxPlayerHealth())
            {
                potionsController.UseHealthPotion();
            }
        }
    }

    void OnMove(InputValue value)
    {
        if (playerAttack.isCrushing)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void MovePlayer()
    {
        if (playerHealth.playerTakenDamage)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        if (moveInput != Vector2.zero)
        {
            lastDirection = moveInput;
            SnapAttackPoint(moveInput);
            if (Time.time >= nextSoundTime)
            {
                AudioSource.PlayClipAtPoint(playerMoveSFX, transform.position, moveVolume);
                nextSoundTime = Time.time + moveSoundDelay;
            }
        }
        animator.SetFloat("LastHorizontal", lastDirection.x);
        animator.SetFloat("LastVertical", lastDirection.y);
    }

    void OnDash(InputValue value)
    {
        if (value.isPressed && skillsController.dashUnlocked && playerStamina.playerStamina >= dashCost)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        playerStamina.DashCost(dashCost);
        isInvincible = true;
        playerCollider.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.linearVelocity = dashSpeed * moveInput.normalized;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        playerCollider.enabled = true;
        isInvincible = false;

    }

    public void UnlockFrenzy()
    {
        frenzyUnlocked = true;
    }

    public void Frenzy()
    {
        if (frenzyUnlocked)
        {
            moveSpeed += speedIncrease;
            Invoke(nameof(ResetSpeed), frenzyTime);
        }
    }

    void ResetSpeed()
    {
        frenzyActive = false;
        moveSpeed = maxMoveSpeed;
    }

    void SnapAttackPoint(Vector2 direction)
    {
        Vector2 snapPos = direction.normalized * offsetDistance;
        attackPoint.transform.localPosition = new Vector3(snapPos.x, snapPos.y, 0f);
    }


}
