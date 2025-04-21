using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor.Build;
using UnityEngine;

public class Atak : MonoBehaviour
{
    public int attackDamaage=10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null )
        {
            bool gotHit= damageable.Hit(attackDamaage);
        }
    }
        
    
}
