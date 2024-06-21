using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Animator _animator;  // Añadir referencia al Animator

    [Space(5f)]
    [Header("Configurations")]
    [SerializeField] private float _speed = 5.0f;          

    private Rigidbody _rb;                                 
    private Vector2 _moveInput;                           

    private void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        if (_cameraTransform == null)
        {
            _cameraTransform = Camera.main.transform;
        }
    }

    private void OnEnable()
    {
        _playerInput.actions["Walk"].performed += OnMove;
        _playerInput.actions["Walk"].canceled += OnMove;
    }

    private void OnDisable()
    {
        _playerInput.actions["Walk"].performed -= OnMove;
        _playerInput.actions["Walk"].canceled -= OnMove;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }


    void FixedUpdate()
    {
        // Calcular la dirección de la cámara
        Vector3 cameraForward = Vector3.Scale(_cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(_cameraTransform.right, new Vector3(1, 0, 1)).normalized;

        // Calcular la dirección de movimiento en relación con la cámara
        Vector3 moveDirection = _moveInput.x * cameraRight + _moveInput.y * cameraForward;

        // Mover al jugador
        _rb.MovePosition(transform.position + moveDirection * _speed * Time.fixedDeltaTime);

        // Girar al jugador en la dirección del movimiento
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * _speed));
        }

        float speed = moveDirection.magnitude;  // Calcular la velocidad como la magnitud de moveDirection
        _animator.SetFloat("Speed", speed);  // Establecer el valor de "Speed"
    }
 
}
