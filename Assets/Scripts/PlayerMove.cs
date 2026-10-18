using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class PlayerMove : MonoBehaviour
{

    [Header("PlayerMove")]
    public float moveSpeed = 8f;

    [SerializeField]
    private InputActionReference moveAction;

    [Header("PlayerJump")]
    public float jumpPower = 10f;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private InputActionReference jumpAction;

     [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Wall Check")]
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallChecRight;
    [SerializeFIeld] private Vector2 wallCheckSize = new Vector2 (0.1f, 3f);
    [SerializeFIeld] private LayerMask wallLayer;
    [SerializeField] private 


     bool isWallLeft;
     bool isWallRight;
     bool isGrounded;
     

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
        
        isWallLeft = Physics2D.OverlapBox(
        wallCheckLeft.position, new Vector2(0.1f, 3f), 0f, wallLayer
       );
        isWallRight = Physics2D.OverlapBox(
        wallCheckRight.position, new Vector2(0.1f, 3f), 0f, wallLayer
        );
        if (!isGrounded && (isWallLeft || isWallRight))
        {

        }
    }

}
