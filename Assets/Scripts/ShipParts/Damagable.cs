using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour {
    [SerializeField] private float maxHealth;

    public UnityEvent OnPartDestroyed;
    public int FactionId { get; set; }

    public void TakeDamage(float dmgAmt) {
        maxHealth -= dmgAmt;

        if (maxHealth <= 0) {
            OnPartDestroyed.Invoke();
        }
    }
}
