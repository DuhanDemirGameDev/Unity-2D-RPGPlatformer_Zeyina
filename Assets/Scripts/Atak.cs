using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Build;
using UnityEngine;

public class Atak : MonoBehaviour
{
    public int attackDamaage=10;
    public Vector2 knockback= Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null )
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x >0 ? knockback : new Vector2( -knockback.x, knockback.y);
            bool gotHit= damageable.Hit(attackDamaage, deliveredKnockback);
        }
    }
        
    
}
