using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damagable))]
public class ShipCore : MonoBehaviour {
    public Action<Connectable> OnPartConnected = delegate { };

    private List<Connectable> connectedParts = new List<Connectable>();
    private KeyCode[] alphaKeyCodes = GetAlphaKeyCodes();
    private Rigidbody2D rb2D;
    private Damagable damagable;
    private Connection[] connections;

    public int FactionId { get; set; }
    public Rigidbody2D Rb2D => rb2D;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        damagable = GetComponent<Damagable>();
        connections = GetComponentsInChildren<Connection>();
        foreach (Connection connection in connections) {
            connection.Ship = this;
        }
    }

    private void Start() {
        rb2D.AddForceAtPosition(Vector2.one * 15f, Vector2.left);
    }

    public void RegisterConnectable(Connectable connectable) {
        if (connectedParts.Contains(connectable)) { return; }

        connectable.ConnectToShip(this);
        connectedParts.Add(connectable);
        connectable.ActivationKey = alphaKeyCodes[UnityEngine.Random.Range(0, alphaKeyCodes.Length)];
        OnPartConnected(connectable);
        Debug.Log($"assigned connectable to key {connectable.ActivationKey.ToString()}");
    }

    public void HandleShipDestroyed() {

    }

    public static KeyCode[] GetAlphaKeyCodes() {
        return new KeyCode[] {
            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.M,
            KeyCode.N,
            KeyCode.O,
            KeyCode.P,
            KeyCode.Q,
            KeyCode.R,
            KeyCode.S,
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.W,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z
        };
    }
}
