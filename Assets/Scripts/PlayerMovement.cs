using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    InputControls controls;

    public SpriteRenderer weaponSpriteRenderer;

    [Header("Collision Check")]
    public Transform frontCheck;
    public Transform groundCheck;
    private float groundCheckWidth;
    public float collisionCheckDistance;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;


    [Header("Attributes")]
    public float movementForce;
    public float jumpForce;
    public Vector2 wallJumpForce;
    public float wallJumpDuration;
    public float wallSlidingSpeed;
    public float maxMovementSpeed;
    public int numberOfConsecutiveJumps;


    [Header("Inner State")]
    private Animator animator;
    private float horizontalMovement;
    private bool isFacingRight;
    private bool isIdle;
    private bool isRunning;
    private bool isGrounded;
    private bool isAttacking;
    private bool isWallSliding;
    private bool isWallJumping;
    private bool isFaceTouching;
    private int usedJumps;

    #region ANIMATOR_PARAMETERS
    private const string PLAYER_GROUNDED = "isGrounded";
    private const string PLAYER_RUNNING = "isRunning";
    private const string PLAYER_JUMP = "jump";
    private const string PLAYER_DOUBLEJUMP = "doubleJump";
    private const string PLAYER_ATTACK = "attack";
    #endregion

    private Rigidbody2D body;
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isFacingRight = true;
        groundCheckWidth = GetComponent<CapsuleCollider2D>().size.x * 0.95f;
    }

    void OnEnable()
    {
        controls = new InputControls();
        controls.Enable();
        controls.Player.move.performed += context => SetMovement(context.ReadValue<float>());
        controls.Player.jump.performed += context => Jump();
        controls.Player.attack.performed += context => StartAttack();
    }

    void OnDisable()
    {
        controls.Disable();
        controls.Player.move.performed -= context => SetMovement(context.ReadValue<float>());
        controls.Player.jump.performed -= context => Jump();
        controls.Player.attack.performed -= context => StartAttack();
    }

    void Update()
    {

        bool tmpIsGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, collisionCheckDistance), 0, whatIsGround);

        if (!isGrounded && tmpIsGrounded && body.velocity.y < 0)
        {
            // just landed on ground
            isGrounded = true;
            usedJumps = 0;
            FindObjectOfType<CameraShake>().SoftShake();
        }
        else
        {
            isGrounded = tmpIsGrounded;
        }
        animator.SetBool(PLAYER_GROUNDED, isGrounded);


        isFaceTouching = Physics2D.OverlapCircle(frontCheck.position, collisionCheckDistance, whatIsGround);
        isWallSliding = isFaceTouching && !isGrounded && Mathf.Abs(horizontalMovement) > 0;
        if (isWallSliding)
        {
            body.velocity = new Vector2(body.velocity.x, Mathf.Clamp(body.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        isRunning = isGrounded && Mathf.Abs(horizontalMovement) > 0;

        animator.SetBool(PLAYER_RUNNING, isRunning);


    }

    void FixedUpdate()
    {
        body.AddForce(horizontalMovement * Vector2.right * maxMovementSpeed);
        body.velocity = new Vector2(Mathf.Clamp(body.velocity.x, -maxMovementSpeed, maxMovementSpeed), body.velocity.y);
    }

    public void SetMovement(float horizontalMovement)
    {
        this.horizontalMovement = horizontalMovement;
        if (horizontalMovement > 0 && !isFacingRight)
        {
            FlipHorizontally();
        }
        else if (horizontalMovement < 0 && isFacingRight)
        {
            FlipHorizontally();
        }
    }

    public void StartAttack()
    {
        animator.SetTrigger(PLAYER_ATTACK);
    }

    /**
    The actual attack is triggered by an animation event.
    */
    public void Attack()
    {
        WeaponSpecs weaponSpecs = GetComponentInChildren<WeaponSpecs>();
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(weaponSpecs.attackTransform.position, weaponSpecs.attackRange, whatIsEnemy);
        if (enemiesToDamage.Length > 0)
        {
            FindObjectOfType<CameraShake>().SoftShake();
        }
        foreach (Collider2D collider in enemiesToDamage)
        {
            collider.GetComponent<Health>().TakeDamage(weaponSpecs.strength);
        }
    }

    public void Jump()
    {
        if (isWallSliding)
        {
            // walljump
            isGrounded = false;
            isWallSliding = false;
            body.AddForce(new Vector2(wallJumpForce.x * -horizontalMovement, wallJumpForce.y), ForceMode2D.Impulse);
            animator.SetTrigger(PLAYER_JUMP);
        }
        else if (isGrounded)
        {
            // normal
            isGrounded = false;
            usedJumps++;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger(PLAYER_JUMP);
        }
        else if (HasRemainingJumps())
        {
            // doublejump
            isGrounded = false;
            usedJumps++;
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger(PLAYER_JUMP);
        }
    }

    public void EquipWeapon(WeaponSpecs weaponSpecs)
    {
        GetComponentInChildren<WeaponSpecs>().attackRange = weaponSpecs.attackRange;
        GetComponentInChildren<WeaponSpecs>().attackTransform.localPosition = weaponSpecs.attackTransform.localPosition;
        GetComponentInChildren<WeaponSpecs>().strength = weaponSpecs.strength;
        GetComponentInChildren<WeaponSpecs>().sprite = weaponSpecs.sprite;
        weaponSpriteRenderer.sprite = weaponSpecs.sprite;
    }

    private bool HasRemainingJumps()
    {
        return numberOfConsecutiveJumps - usedJumps > 0;
    }

    private void FlipHorizontally()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.92f, 0.016f, 0.5f);
        Gizmos.DrawCube(groundCheck.position, new Vector3(groundCheckWidth, collisionCheckDistance, 1));
        Gizmos.DrawSphere(frontCheck.position, collisionCheckDistance);
    }
}
