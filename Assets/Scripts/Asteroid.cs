using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : ObjectBase {
    private Rigidbody2D rb2D;

    public Rigidbody2D Rb2D => rb2D;

    public float AsteroidTier = 5;

    public delegate void AsteroidDestroy(Asteroid asteroid);
    public event AsteroidDestroy DestroyEvent;

    Collider2D collider;
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rb2D.AddForceAtPosition(Vector2.one * 100f, Vector2.left);
        transform.localScale += (AsteroidTier * Vector3.one);
    }


    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.name);
        Damagable damagedObject = other.GetComponent<Damagable>();
        Bullet bullet = other.GetComponent<Bullet>();

        if (damagedObject != null) {
            damagedObject.TakeDamage(1);
            OnAsteroidHit();
        } else if (bullet != null) {
            OnAsteroidHit();
        }
    }

    private void OnAsteroidHit() {
        Destroy(this.gameObject);
    }
}
