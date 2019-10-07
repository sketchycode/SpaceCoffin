using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damagable))]
public class ShipCore : ObjectBase {
    public float ThrustFactor = 5.0f;
    public float TurnSpeed = 2f;

    public int FactionId { get; set; }
    public Rigidbody2D Rb2D => rb2D;
    public Action<Connectable> OnPartConnected = delegate { };

    private List<Connectable> connectedParts = new List<Connectable>();
    private KeyCode[] alphaKeyCodes = GetAlphaKeyCodes();
    private Rigidbody2D rb2D;
    private Damagable damagable;
    private Connection[] connections;
    private Vector2 thrustInput;
    private bool isBraking = false;

    private void Awake() {
        rb2D = GetComponent<Rigidbody2D>();
        damagable = GetComponent<Damagable>();
        connections = GetComponentsInChildren<Connection>();
        foreach (Connection connection in connections) {
            connection.Ship = this;
        }
    }

    protected override void Start() {
        base.Start();
        rb2D.AddForceAtPosition(Vector2.one * 15f, Vector2.left);
    }

    protected override void Update() {
        base.Update();
        CheckBaseThrusters();
    }

    private void LateUpdate() {
        rb2D.AddForce(transform.up * thrustInput.y * ThrustFactor);
        rb2D.AddTorque(thrustInput.x * TurnSpeed);
        rb2D.drag = isBraking ? 5f : 0.05f;
        rb2D.angularDrag = isBraking ? 5f : 0.05f;
    }

    private void CheckBaseThrusters() {
        thrustInput.y = Input.GetKey(KeyCode.W) ? 1f : 0;
        thrustInput.x = 0;
        thrustInput.x += Input.GetKey(KeyCode.A) ? 1f : 0;
        thrustInput.x += Input.GetKey(KeyCode.D) ? -1f : 0;
        isBraking = Input.GetKey(KeyCode.S);
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
            KeyCode.B,
            KeyCode.C,
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
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z
        };
    }

    private void ThrustActive(Transform thrustPosition) {
        this.Rb2D.AddForceAtPosition(transform.up * ThrustFactor, thrustPosition.position);
    }

    private void ThrustActivating(Transform thrustPosition) {
        //add thrust fire sprite
    }

    private void ThrustDeactivated(Transform thrustPosition) {
        // rm thrust fire sprite
    }
}
