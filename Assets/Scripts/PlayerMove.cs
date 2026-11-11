using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMove : MonoBehaviour
{
    [Header("CommonSetting")]
    [SerializeField]
    private Rigidbody2D rb;
    private float defaultGravityScale = 10;

    [Header("PlayerMove")]
    public float moveSpeed = 8f;

    [SerializeField]
    private InputActionReference moveAction;

    [Header("PlayerJump")]
    public float jumpPower = 10f;

    

    [SerializeField]
    private InputActionReference jumpAction;

     [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Wall")]
    [SerializeField] private Collider2D wallCollider;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Collider2D wallCheckLeft;
    [SerializeField] private Collider2D wallCheckRight;
   

       private bool isGrounded;
       private bool isWallLeft;
       private bool isWallRight;

       private bool isWallHang;
       


    private void Update()
    {   //이동
        Vector2 moveinput = moveAction.action.ReadValue<Vector2>();

          rb.linearVelocity = new Vector2(moveinput.x * moveSpeed, rb.linearVelocity.y);

        //땅 체크
        isGrounded = Physics2D.OverlapCircle(
        groundCheck.position,
        groundCheckRadius,
        groundLayer
        ) != null;
        //점프
        if (jumpAction.action.WasPressedThisFrame() && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
        
        if (jumpAction.action.WasReleasedThisFrame() && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
        //벽타기 
        isWallLeft = wallCheckLeft.IsTouchingLayers(wallLayer);
        isWallRight = wallCheckRight.IsTouchingLayers(wallLayer);
        
        
       
        

    }
}
        
        