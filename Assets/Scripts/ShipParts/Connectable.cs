using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damagable))]
public class Connectable : MonoBehaviour {
    public virtual KeyCode ActivationKey { get; set; }

    private Connection[] connections;

    protected Damagable damagable;
    protected ShipCore connectedShip = null;
    protected int factionId;
    protected Rigidbody2D rb2D;

    protected virtual void Awake() {
        connections = GetComponentsInChildren<Connection>();
        foreach (var connection in connections) {
            connection.gameObject.SetActive(false);
        }
        damagable = GetComponent<Damagable>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() {
        if (connectedShip != null) {
            if (Input.GetKeyDown(ActivationKey)) {
                OnPartActivated();
                OnPartActivating();
            }
            if (Input.GetKey(ActivationKey)) {
                OnPartActivating();
            } else if (Input.GetKeyUp(ActivationKey)) {
                OnPartDeactivated();
            }
        }
    }

    public void ConnectToShip(ShipCore ship) {
        Debug.Log($"connecting {name} to ship");
        connectedShip = ship;
        factionId = ship.FactionId;
        foreach (var connection in connections) {
            connection.gameObject.SetActive(true);
            connection.Ship = ship;
        }

        damagable.FactionId = factionId;
        rb2D.isKinematic = true;
        OnShipConnected();
    }

    public void DisconnectFromShip() {
        OnShipDisconnected();
    }

    protected virtual void OnShipConnected() { }
    protected virtual void OnShipDisconnected() { }
    protected virtual void OnPartActivated() { }
    protected virtual void OnPartActivating() { }
    protected virtual void OnPartDeactivated() { }

    public void HandlePartDestroyed() {

    }
}
