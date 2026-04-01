using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    public bool isActive;
    
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator _animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActive) return;
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 0)
            _animator.SetBool(IsMoving, true);
        else
            _animator.SetBool(IsMoving, false);
    }

    void FixedUpdate()
    {
        if (!isActive) return;
        
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}