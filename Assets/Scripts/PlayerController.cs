using System.Collections;
using UnityEngine;



// Fix bug where the player animation doesn't player properly when the gravity has shifted
// Optimize the code/naming conventions 

// name hardcoded variables

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    [Header("Jumping")]
    [SerializeField] private KeyCode jumpKey;
    [SerializeField] private float jumpHeight = 3f;
    private bool jumping = false;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime = 0.35f;

    [Header("GroundCheck")]
    [SerializeField] private GameObject groundCheckPosition;
    [SerializeField] private float groundCheckSize = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    private bool grounded;

    [Header("Movement")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float airMultiplier = 0.25f;
    private Vector2 moveDirection;

    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float maxDownwardVelocity = -10f;

    [SerializeField] private LashingAbility_SO lashingAbility_SO;

    private bool gravityInverted = false;
    private Vector2 groundDirection = Vector2.down * 2f;


    public bool Grounded => grounded;
    private void OnEnable()
    {
        if (lashingAbility_SO != null)
        {
            lashingAbility_SO.onAbilityPressed += FlipCharacter;
            lashingAbility_SO.onAbilityReleased += FlipCharacter;
        }
    }

    private void OnDisable()
    {
        if (lashingAbility_SO != null)
        {
            lashingAbility_SO.onAbilityPressed -= FlipCharacter;
            lashingAbility_SO.onAbilityReleased -= FlipCharacter;
        }
    }

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (lashingAbility_SO != null)
        {
            groundDirection = lashingAbility_SO.gravityDirection;
        }
        gravityInverted = groundDirection.x != 0;
        GroundCheck();
        MovePlayer();
        Jump();
        ApplyGravity();
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(groundCheckPosition.transform.position, Vector2.one * groundCheckSize, 0f, groundDirection, groundCheckSize, groundLayer);
        grounded = hit.collider != null;
    }

    private void ApplyGravity()
    {
        if (!gravityInverted)
        {
            moveDirection.y += gravity * groundDirection.y * Time.deltaTime;
            moveDirection.y = Mathf.Clamp(moveDirection.y, maxDownwardVelocity, Mathf.Abs(maxDownwardVelocity));
        }
        else
        {
            moveDirection.x += gravity * groundDirection.x * Time.deltaTime;
            moveDirection.x = Mathf.Clamp(moveDirection.x, maxDownwardVelocity, Mathf.Abs(maxDownwardVelocity));
        }
    }

    private void Jump()
    {
        // Determine the sign based on the direction of the ground.
        // If the ground direction in y or x is negative, the sign is 1, otherwise -1.
        int sign = groundDirection.y < 0  || groundDirection.x < 0 ? 1 : -1;
        if (grounded && Input.GetKeyDown(jumpKey))
        {
            jumping = true;
            jumpTimeCounter = jumpTime;
            if (!gravityInverted) moveDirection.y = sign * jumpHeight;
            else moveDirection.x = sign * jumpHeight;
        }

        if (Input.GetKey(jumpKey) && jumping)
        {
            if (jumpTimeCounter > 0)
            {
                if (!gravityInverted) moveDirection.y = sign * jumpHeight;
                else moveDirection.x = sign * jumpHeight;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if (Input.GetKeyUp(jumpKey))
        {
            jumping = false;
        }
    }

    private void MovePlayer()
    {
        float airSpeed = grounded ? 1f : airMultiplier;
        float horizontalMove = Input.GetAxis("Horizontal");

        if (!gravityInverted)
        {
            moveDirection.x = horizontalMove * airSpeed;
        }
        else
        {
            moveDirection.y = horizontalMove * airSpeed;
        }


        if (moveDirection.x > 0f) spriteRenderer.flipX = true;
        else if (moveDirection.x < 0f) spriteRenderer.flipX = false;
    }

    private void FixedUpdate()
    {
        if (!gravityInverted) rb2D.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime* 30f, moveDirection.y * Time.deltaTime * 60f);
        else rb2D.velocity = new Vector2(moveDirection.x * Time.deltaTime * 60f, moveDirection.y * speed * Time.deltaTime * 30f);
    }

    private void FlipCharacter()
    {
        StopAllCoroutines();
        StartCoroutine(RotateCharacter());
    }

    private IEnumerator RotateCharacter()
    {
        float currentTime = 0f;
        // duration of the rotation animation
        float duration = 0.5f;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (!gravityInverted)
        {
            endRotation = Quaternion.Euler(0f, 0f, groundDirection.y < 0 ? 0f : 180f);
        }
        else
        {
            endRotation = Quaternion.Euler(0f, 0f, groundDirection.x < 0 ? 270f : 90f);
        }

        while (currentTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, currentTime / duration);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        //rb2D.freezeRotation = true;
    }
}
