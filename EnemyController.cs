using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool startRight;

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    bool dead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        dead = false;

        if (!startRight)
            speed *= -1;
    }

    void FixedUpdate()
    {
        if (dead)
            return;
        UpdateDirection();
        UpdateMovement();
    }

    void UpdateDirection()
    {
        if (speed != 0)
            transform.localScale = new Vector3(Mathf.Sign(speed), transform.localScale.y, transform.localScale.z);
    }

    void UpdateMovement()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
      

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Barrier"))
            speed *= -1;
    }

    public void Die()
    {
        anim.SetTrigger("Death");
        StartCoroutine(DelayedDeath());
    }

    IEnumerator DelayedDeath()
    {
        dead = true;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);

        Destroy(gameObject);
    }
}




