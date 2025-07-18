using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 10f;
    [SerializeField] private int health = 3;

    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float leftBound = startPosition.x - distance;
        float rightBound = startPosition.x + distance;
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound)
            {
                movingRight = true;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy HP: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        GameManager.Instance.AddScore(10);
        Destroy(gameObject);
    }
}
