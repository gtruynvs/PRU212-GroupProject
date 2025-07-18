using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float invincibilityDuration = 2f;
    private float lastDamageTime = -Mathf.Infinity;
    private bool isInvincible = false;

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool isGrounded = false;

    private Animator animator;

    public int health = 3;
    private bool isDead = false;

    private void Start()
    {
        isDead = false;
        UIController.Instance?.UpdateHearts(health);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;
        Move();
        Jump();
        UpdateAnimation();
    }

    private void Move()
    {
        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(moveInput) * Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
    }

    private void Jump()
    {
        if (isDead) return;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        animator.SetBool("isRunning", isRunning);
        bool isJumping = !isGrounded && rb.linearVelocity.y > 0.1f;
        animator.SetBool("isJumping", isJumping);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible || isDead) return;

        health -= damage;

        isInvincible = true;
        UIController.Instance?.UpdateHearts(health);

        if (health <= 0)
        {
            isDead = true;
            rb.linearVelocity = Vector2.zero;
            rb.isKinematic = true;
            GameManager.Instance.GameOver();
        }

        else
        {
            StartCoroutine(InvincibilityFlash());
        }
    }


    private IEnumerator InvincibilityFlash()
    {
        isInvincible = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float elapsed = 0f;
        float flashInterval = 0.1f;

        while (elapsed < invincibilityDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        sr.enabled = true;
        isInvincible = false;
    }




    public void PlayerHealth(int addHealth)
    {
        health = health + addHealth;
        if (health <= 0)
        {
            Debug.Log("Player is dead!");
            GameManager.Instance.GameOver();
        }
        else
        {
            Debug.Log("Player health: " + health);
        }
    }


}
