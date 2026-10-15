
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float horizontalinput;
    float moveSpeed = 10f;

    Rigidbody2D rb;
    Animator animator;

    public event Action PlayerDied;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalinput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed ||
                Keyboard.current.leftArrowKey.isPressed)
            {
                horizontalinput = -1f;
            }
            else if (Keyboard.current.dKey.isPressed ||
                     Keyboard.current.rightArrowKey.isPressed)
            {
                horizontalinput = 1f;
            }
        }

        // Hubungkan input gerakan dengan animasi
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalinput));
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            horizontalinput * moveSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            PlayerDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
