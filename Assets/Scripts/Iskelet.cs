using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Iskelet : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public enum WalkableDirection { Right, Left };
    public WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public float flipCooldown = 0.5f; // Y�n de�i�tirme i�in bekleme s�resi (yar�m saniye)
    private float lastFlipTime = 0f; // Son d�n�� zaman�


    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    public DetectionZone attackZone;
    public DetectionZone cliffZone;

    Animator animator;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Y�n de�i�ti�inde hem walkDirectionVector'� hem de localScale'i ayarla
                gameObject.transform.localScale = new Vector2
                    (Mathf.Abs(gameObject.transform.localScale.x) *
                    (value == WalkableDirection.Right ? 1 : -1),
                     gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; // Y�n� g�ncelle
        }
    }

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }
    private void FixedUpdate()
    {
        // Duvara �arpt���nda y�n� de�i�tir
        if (touchingDirections.IsOnWall && touchingDirections.IsGrounded || cliffZone.detectedColliders.Count == 0)
        {
            if (Time.time >= lastFlipTime + flipCooldown)
            {
                FlipDirection();
                lastFlipTime = Time.time;
            }
        }

        if (CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
            rb.velocity= new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }
    }

    private void FlipDirection()
    {
        // Y�n de�i�tir
        WalkDirection = (WalkDirection == WalkableDirection.Right) ? WalkableDirection.Left : WalkableDirection.Right;
    }
}
