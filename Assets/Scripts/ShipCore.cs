using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipCore : MonoBehaviour {
    private void Start() {
        var rb2D = GetComponent<Rigidbody2D>();
        rb2D.AddForceAtPosition(Vector2.one * 5f, Vector2.left);
    }
}
