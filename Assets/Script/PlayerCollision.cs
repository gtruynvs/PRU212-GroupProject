using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;

    [SerializeField] private float bounceForce = 7f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger with: " + collision.tag);

        if (collision.CompareTag("Point"))
        {
            Debug.Log("Collected a point!");
            GameManager.Instance.AddScore(1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Enemy"))
        {
            Transform enemyTransform = collision.transform;
            bool isAbove = transform.position.y > enemyTransform.position.y + 0.3f;
            bool isFalling = rb.linearVelocity.y < 0;

            if (isAbove && isFalling)
            {
                Debug.Log("Stomped enemy!");
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1); // Giảm 1 máu mỗi lần đạp
                }

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);
            }
            else
            {
                Debug.Log("Hit enemy from side!");
                playerController.TakeDamage(1);
            }
        }

        else if (collision.CompareTag("Trap"))
        {
            Debug.Log("Hit trap!");
            playerController.TakeDamage(1);
        }
        else if (collision.CompareTag("Finish"))
        {
            Debug.Log("Reached finish line!");
            GameManager.Instance.Win(); // Gọi hàm chiến thắng
        }

    }

}
