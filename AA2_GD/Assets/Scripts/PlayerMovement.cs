using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    public bool isActive;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Aiming Indicator")]
    public Transform aimIndicator; // Arrastra aquí la bolita/mirilla desde el Inspector
    public float orbitRadius = 1.5f; // Distancia a la que orbita

    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator _animator;
    private Camera mainCam;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }

    void Update()
    {
        if (!isActive) return;

        // 1. Inputs de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        _animator.SetBool(IsMoving, movement.magnitude > 0);

        // 2. Apuntar y girar al personaje
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        FlipCharacter(mousePos);
        PositionAimIndicator(mousePos);
    }

    void FixedUpdate()
    {
        if (!isActive) return;

        // Movimiento normalizado
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void FlipCharacter(Vector3 mousePos)
    {
        Vector3 scale = transform.localScale;

        // Si el ratón está a la izquierda, escala negativa en X. Si no, positiva.
        if (mousePos.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    private void PositionAimIndicator(Vector3 mousePos)
    {
        if (aimIndicator == null) return;

        // Calcula la dirección y pone la mira en el borde del radio
        Vector2 lookDir = (mousePos - transform.position).normalized;
        aimIndicator.position = (Vector2)transform.position + (lookDir * orbitRadius);
    }
}