using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{

    private Controls playerActionControls;
    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private AudioSource sfx;

    private void Awake()
    {
        playerActionControls = new Controls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }

    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    void Start()
    {
        playerActionControls.Player2D.Jump.performed += _ => Jump();
    }

    private void Jump() //Self Explanitory, Jump stuff.
    {
        if (IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            sfx.PlayOneShot((AudioClip)Resources.Load("Sounds/General/2D/Jump"));
        }
    }

    private bool IsGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += col.bounds.extents.x;
        bottomRightPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRightPoint, ground);
    }

    void FixedUpdate()
    {
        Move();
        if (!IsGrounded()) animator.SetBool("Jump", true);
        if (IsGrounded()) animator.SetBool("Jump", false);
    }

    private void Move()
    {
        // Reads the movement value
        float movementInput = playerActionControls.Player2D.Move.ReadValue<float>();
        // Moves Mario... Luigi will be there later... hopefully.
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;
        transform.position = currentPosition;

        // Animator stuffs
        if (movementInput > 0)
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("Walk", true);
        }
        else if (movementInput == 0) animator.SetBool("Walk", false);

        if (movementInput < 0)
        {
            animator.SetBool("Walk", true);
            animator.SetBool("isLeft", true);
        }
        else if (movementInput == 0) animator.SetBool("Walk", false);
    }
}
