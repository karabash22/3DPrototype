using UnityEngine;

public class BonusTrigger : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerParameter = "IsInZone";
    
    [Header("Debug")]
    [SerializeField] private bool showDebug = true;

    private void Start()
    {
        // Автоматически получаем Animator если не назначен
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        // Проверка наличия Animator
        if (animator == null)
        {
            if (showDebug)
                Debug.LogError("Animator component not found on Bonus object!", this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это игрок
        if (other.CompareTag("Player"))
        {
            if (showDebug)
                Debug.Log("Player entered bonus zone", this);
            
            // Активируем анимацию
            if (animator != null)
            {
                animator.SetBool(triggerParameter, true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Проверяем, что это игрок
        if (other.CompareTag("Player"))
        {
            if (showDebug)
                Debug.Log("Player exited bonus zone", this);
            
            // Деактивируем анимацию
            if (animator != null)
            {
                animator.SetBool(triggerParameter, false);
            }
        }
    }
}