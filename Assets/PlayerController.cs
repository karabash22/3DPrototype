using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody rb;
    private PlayerControls playerControls;
    private float moveX;
    private float moveY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerControls = new PlayerControls();
        
        // ВАЖНО: Используем ReadValue<float>() для осей
        playerControls.Player.MoveX.performed += ctx => moveX = ctx.ReadValue<float>();
        playerControls.Player.MoveX.canceled += ctx => moveX = 0;
        
        playerControls.Player.MoveY.performed += ctx => moveY = ctx.ReadValue<float>();
        playerControls.Player.MoveY.canceled += ctx => moveY = 0;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        // Создаем вектор движения из отдельных осей
        Vector3 move = new Vector3(moveX, 0, moveY) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
}