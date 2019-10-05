using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ConnectPoint : MonoBehaviour {
    public Action<ConnectPoint, Connectable> OnConnected = delegate { };

    private Collider2D coll2D;

    private void Awake() {
        coll2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var connectable = other.GetComponent<Connectable>();
        if (connectable) {
            OnConnected(this, connectable);
            coll2D.gameObject.SetActive(false);
        }
    }
}
