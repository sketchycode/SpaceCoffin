using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour {
    [SerializeField] private float maxHealth;

    public UnityEvent OnPartDestroyed;
    public int FactionId { get; set; }
    public bool IsDestroyed { get; private set; } = false;

    private float currentHealth;
    private ParticleSystem[] explosionEffects;

    private void Awake() {
        explosionEffects = GetComponentsInChildren<ParticleSystem>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmgAmt) {
        Debug.Log($"{name} took {dmgAmt} damage");
        currentHealth -= dmgAmt;

        if (currentHealth <= 0 && !IsDestroyed) {
            foreach (var effect in explosionEffects) {
                effect.transform.SetParent(null, true);
                effect.Play();
            }
            IsDestroyed = true;
            Debug.Log($"{name} destroyed");
            OnPartDestroyed.Invoke();
        }
    }
}
