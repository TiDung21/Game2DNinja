using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamge
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [Header("Sound")]
    [SerializeField] private AudioClip impactSound;

    private float checkTimer;
    private bool attack;

    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        StopAttack();
    }
    private void Update()
    {
        if (attack)
        {
            transform.Translate(destination * speed * Time.deltaTime);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attack)
            {
                attack = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void StopAttack()
    {
        destination = transform.position;
        attack = false;
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = - transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = - transform.up * range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(collision);
        StopAttack();
    }
}
