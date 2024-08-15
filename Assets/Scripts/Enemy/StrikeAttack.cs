using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeAttack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    private bool attackStrike;
    private Vector3 targetPos;
    // Start is called before the first frame update
    private void Start()
    {
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //if (IsWithinStrikeRange())
        //{
        //    attack = true;
        //    animator.SetTrigger("strikeAttack");
        //    transform.position = Vector3.Lerp(transform.position, player.position, speed / (Mathf.Abs(transform.position.x - player.position.x)) * Time.deltaTime);
        //    attack = false;
        //}

        if(attackStrike)
        {
            targetPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, speed / (Mathf.Abs(transform.position.x - player.position.x)) * Time.deltaTime);
            animator.SetTrigger("strikeAttack");
            attackStrike = false;
        }
        else
        {
            CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x,0), range, playerLayer);
        if(hit.collider != null && !attackStrike)
        {
            attackStrike = true;
        }
    }
}
