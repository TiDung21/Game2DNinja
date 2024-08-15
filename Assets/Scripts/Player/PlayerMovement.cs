using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundedLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D playerRb;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private float horizontalInput;
    private float wallCoolDown;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if( horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        

        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());

        if(wallCoolDown <0.2f)
        {
            playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);

            
            if (OnWall() && !isGrounded())
            {
                playerRb.gravityScale = 0;
                playerRb.velocity = Vector2.zero;
            }
            else
            {
                playerRb.gravityScale = 5;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
                if(isGrounded())
                {
                    SoundManager.instance.PlaySound(jumpSound);
                }
            }
        }
        else
        {
            wallCoolDown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            animator.SetTrigger("jump");
        }
        else if (OnWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                playerRb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 15, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), (transform.localScale.y), (transform.localScale.z));
            }
            else
            {
                playerRb.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 10);
            }
            wallCoolDown = 0;

        }


    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundedLayer);
        return raycastHit.collider != null;
    }
    private bool OnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && !OnWall() && isGrounded()  ;
    }
}
