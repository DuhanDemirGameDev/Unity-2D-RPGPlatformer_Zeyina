using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent <int, Vector2> damagableHit;
    Animator animator;

    [SerializeField]
    private int _maxHealth;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool isInvincible = false;

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }

    }

    private float timeSinceHit=0;
    public float invincibilityTime = 0.25f;

    private bool _isAlive = true;

    public bool IsAlive
    {
        get 
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit=0;
            }

            timeSinceHit += Time.deltaTime;
        }

    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible=true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            damagableHit?.Invoke(damage, knockback);
            return true;
        }
        return false;
    }

   
}
