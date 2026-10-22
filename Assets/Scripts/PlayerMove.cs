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
    [SerializeField] private Vector2 wallCheckSize = new Vector2 (0.1f, 3f);
    [SerializeField] private float wallCheckRadius = 0f;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallHangTime = 0.15f;

    float wallHangTimer;
    bool isWallAttached;
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
        wallCheckLeft.position, wallCheckSize, wallCheckRadius, wallLayer);
        isWallRight = Physics2D.OverlapBox(
        wallChecRight.position, wallCheckSize, wallCheckRadius, wallLayer);
        bool isPushingWall = (isWallLeft && moveinput.x < -0.01f) || (isWallRight && moveinput.x > 0.01f);
        bool isPushingAway = (isWallLeft && moveinput.x > -0.01f) || (isWallRight && moveinput.x < 0.01f);

        
        if (isGrounded || isPushingAway || (!isWallLeft && !isWallRight))
        {
            isWallAttached = false;
        }
        else if(isPushingWall)
        {
            isWallAttached = true;
        }
        if(isWallAttached && rb.linearVelocity.y <= 0f)
        {
            wallHangTimer += Time.deltaTime;
            
            if(wallHangTimer < wallHangTime)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed));
            }
        }
        else
        {
            wallHangTimer = 0f;
        }
    }

}
