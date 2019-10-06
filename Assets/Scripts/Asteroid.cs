using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : ObjectBase
{
    private Rigidbody2D rb2D;

    public Rigidbody2D Rb2D => rb2D;

    public float AsteroidTier = 5;

    public delegate void AsteroidDestroy(Asteroid asteroid);
    public event AsteroidDestroy DestroyEvent;

    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rb2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        rb2D.AddForceAtPosition(Vector2.one * 100f, Vector2.left);
        transform.localScale += (AsteroidTier * Vector3.one);
    }


    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        Damagable damagedObject = collision.collider.GetComponent<Damagable>();
        if (damagedObject != null)
        {
            damagedObject.TakeDamage(1);
            OnAsteroidHit();
        }
    }

    private void OnAsteroidHit()
    {
        DestroyEvent(this);
        Destroy(this.gameObject);
    }

}
