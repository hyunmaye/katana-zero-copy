using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField]private InputActionReference jumpAction;

    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    private bool isJumping;


   

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb=GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        //이동 애니메이션
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
        
        bool isRunPressed = Mathf.Abs(moveInput.x) > 0.01f;
        animator.SetBool("isRun", isRunPressed);

        if (moveInput.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        //점프 애니메이션

        bool isGrounded =
            Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            ) !=null;
        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            isJumping = true;
        }
        else if (isGrounded && rb.linearVelocity.y <= 0f)
        {
            isJumping = false;
        }
        else if (rb.linearVelocity.y < -0.01f)
        {
            isJumping = false;
        }
         animator.SetBool("isRun", isRunPressed);
         animator.SetBool("isJump", isJumping);
         animator.SetBool("isGrounded", isGrounded);
    } 
}

