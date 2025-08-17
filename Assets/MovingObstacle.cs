using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Vector3 moveDirection = Vector3.right;
    [SerializeField] private float moveDistance = 10f;
    
    [Header("Damage Settings")]
    [SerializeField] private float damageToPlayer = 15f;
    [SerializeField] private float obstacleDamage = 10f;
    
    [Header("Debug")]
    [SerializeField] private bool showDebug = true;
    
    private Vector3 startPosition;
    private bool movingForward = true;

    private void Start()
    {
        startPosition = transform.position;

        if (showDebug)
        {
            Debug.Log($"{gameObject.name} (Cylinder) started. Position: {startPosition}", this);
        }
    }

    private void FixedUpdate()
    {
        float distanceMoved = Vector3.Distance(startPosition, transform.position);
        
        if (distanceMoved >= moveDistance)
        {
            movingForward = !movingForward;
        }
        
        Vector3 direction = movingForward ? moveDirection : -moveDirection;
        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Столкновение с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            if (showDebug)
            {
                Debug.Log($"{gameObject.name} HIT PLAYER! Applying {damageToPlayer} damage", this);
            }
            
            // Наносим урон игроку
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
                if (showDebug)
                {
                    Debug.Log($"Player health after damage: {playerHealth.GetCurrentHealth()}", this);
                }
            }
            else
            {
                if (showDebug)
                {
                    Debug.LogWarning($"PlayerHealth component NOT FOUND on {collision.gameObject.name}!", this);
                }
            }
        }
    }
}