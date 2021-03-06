﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private float accelerationForce;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private Rigidbody2D rb2d;

    [SerializeField]
    private Collider2D playerGroundCollider;

    [SerializeField]
    private PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    [SerializeField]
    private Collider2D groundDetectTrigger;

    [SerializeField]
    private ContactFilter2D groundContactFliter;

    [SerializeField]
    private Text deathText;

    private bool isFacingRight = true;
    private float horizontalInput;
    private bool isOnGround;
    private bool doubleJump = false;
    private bool isDead;

    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    private Checkpoint currentCheckpoint;
    private Animator animator;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    //Update is called once per frame
    private void Update()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        CheckForRespawn();
    }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
    }

    private void UpdatePhysicsMaterial()
    {
        if(Mathf.Abs(horizontalInput)>0)
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;

        else
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFliter, groundHitDetectionResults)> 0;
        animator.SetBool("Ground", isOnGround);
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJumpInput()
    {
        animator.SetFloat("VerticalSpeed", rb2d.velocity.y);

        if (Input.GetButtonDown("Jump") && (isOnGround || !doubleJump))
        {
            animator.SetBool("Ground", false);
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            if (!doubleJump && !isOnGround)
                doubleJump = true;
        }
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (isOnGround)
            doubleJump = false;
        if (horizontalInput > 0 && !isFacingRight)
            Flip();
        if (horizontalInput < 0 && isFacingRight)
            Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Die()
    {
        isDead = true;
        animator.SetBool("OnHazard", true);
        audioSource.Play();
    }

    private void Respawn ()
    {
        isDead = false;
        animator.SetBool("OnHazard", false);
        deathText.text = null;
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (currentCheckpoint == null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
        }
    }
    private void CheckForRespawn()
    {
        if (isDead)
        {
            deathText.text = "Press R to respawn";
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            if (Input.GetButtonDown("Respawn"))
            {
                Respawn();
            }
        }
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
}
