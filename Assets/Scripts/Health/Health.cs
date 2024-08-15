using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;

    [Header("iFrames")]
    [SerializeField] private float notTakeDamageTime;
    [SerializeField] private int numOfFlashes;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    public float curHealth { get; private set; }
    private bool dead;
    private bool invulnerable;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        curHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        curHealth = Mathf.Clamp(curHealth - _damage, 0, startingHealth);

        if (curHealth > 0 )
        {
            animator.SetTrigger("hurt");
            StartCoroutine(NotTakeDamage());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {

                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                animator.SetBool("grounded", true);
                animator.SetTrigger("die");
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        curHealth = Mathf.Clamp(curHealth + _value, 0, startingHealth);
    }

    public void RespawnPlayer()
    {
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(NotTakeDamage());

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }

    private IEnumerator NotTakeDamage()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(notTakeDamageTime/ (numOfFlashes *2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(notTakeDamageTime / (numOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
        
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
