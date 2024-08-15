using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip meleeAttackSound;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Health playerHealth;

    private float cooldownTimer = Mathf.Infinity;
    private Vector2 distanceAttack;
    private Vector3 sizeBoxAttack;

    private Animator animator;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight())
        {
            if (cooldownTimer > attackCooldown && playerHealth.curHealth>0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(meleeAttackSound);
            }
        }
        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
        
    }

    private bool PlayerInSight()
    {
        sizeBoxAttack = new Vector2(boxCollider.bounds.size.x * range , boxCollider.bounds.size.y);
        distanceAttack = boxCollider.bounds.center + transform.right * transform.localScale.x * range * colliderDistance;
        RaycastHit2D hit = Physics2D.BoxCast(distanceAttack, sizeBoxAttack, 0, Vector2.left, 0, playerLayer);
        if(hit.collider !=null)
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        sizeBoxAttack = new Vector2(boxCollider.bounds.size.x * range , boxCollider.bounds.size.y);
        distanceAttack = boxCollider.bounds.center + transform.right * transform.localScale.x * range * colliderDistance;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(distanceAttack, sizeBoxAttack);
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
