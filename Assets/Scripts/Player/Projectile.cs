using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private float lifeTime;
    private bool hit;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private Vector3 localScale;

    private void Awake()
    {
        localScale = new Vector3(0.7f, 0.7f, 0.7f);
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed* Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
        lifeTime += Time.deltaTime;
        if ( lifeTime > 5 )
        {
            Deactivate();
        } 

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");

        if(collision.tag =="Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, localScale.y, localScale.z);
        Debug.Log("localScale1:" + transform.localScale);
    }
    public void SetScale(float _scale)
    {
        float scaleRate = _scale;
        transform.localScale = new Vector3(localScale.x * scaleRate, localScale.y * scaleRate, localScale.z * scaleRate);
        //boxCollider.size = new Vector2(1,1);
        Debug.Log("localScale2:" + transform.localScale + "\n scaleRate: " + scaleRate );

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
