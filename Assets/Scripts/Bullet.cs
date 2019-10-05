using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifeTime = 4f;
    [SerializeField] private float damageAmount = 1f;

    private Rigidbody2D rb2D;

    public int FactionId { get; set; }

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        rb2D.velocity = transform.rotation * (Vector2.up * speed);
        GameObject.Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var damagable = other.GetComponent<Damagable>();
        if (damagable != null && damagable.FactionId != FactionId) {
            damagable.TakeDamage(damageAmount);
        }
    }
}
