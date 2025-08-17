using UnityEngine;

public class Barrel : MonoBehaviour
{
    [Header("Explosion Settings")]
    [SerializeField] private float explosionRadius = 5f;         // Радиус взрыва
    [SerializeField] private LayerMask explosionLayers = ~0;     // Слои, на которые действует взрыв
    
    [Header("Damage Settings")]
    [SerializeField] private float minImpactForce = 10f;         // Минимальная сила для взрыва
    [SerializeField] private float playerDamage = 25f;           // Урон игроку при взрыве
    
    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem explosionEffect;     // Эффект взрыва
    
    [Header("Debug")]
    [SerializeField] private bool showDebug = true;

    private bool isExploded = false; // Защита от повторного взрыва

    private void OnCollisionEnter(Collision collision)
    {
        // Защита от повторного взрыва
        if (isExploded) return;
        
        // Проверяем, что столкновение с игроком
        if (collision.gameObject.CompareTag("Player"))
        {
            // Рассчитываем силу удара
            float impactForce = collision.impulse.magnitude / Time.fixedDeltaTime;
            
            if (showDebug)
                Debug.Log($"Barrel hit! Impact force: {impactForce}", this);
            
            // Если сила удара достаточная - взрываем
            if (impactForce >= minImpactForce)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        if (isExploded) return;
        isExploded = true;
        
        if (showDebug)
            Debug.Log("Barrel exploded!", this);
        
        // Проигрываем визуальный эффект
        if (explosionEffect != null)
        {
            var effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }
        
        // Наносим урон игроку в радиусе взрыва
        DealExplosionDamage();
        
        // Уничтожаем бочку
        Destroy(gameObject);
    }

    private void DealExplosionDamage()
    {
        // Находим всех игроков в радиусе взрыва
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayers);
        
        foreach (var col in hitColliders)
        {
            // Проверяем, если это игрок
            if (col.CompareTag("Player"))
            {
                // Ищем компонент здоровья у игрока
                PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(playerDamage);
                    if (showDebug)
                        Debug.Log($"Player damaged by explosion: {playerDamage}", this);
                }
            }
        }
    }

    // Визуализация радиуса взрыва в редакторе
    private void OnDrawGizmosSelected()
    {
        if (showDebug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }
}