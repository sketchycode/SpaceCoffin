using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Damagable))]
public class Connectable : ObjectBase {
    [SerializeField] private float chainDestroyDelay = 0.3f;

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
        damagable.OnPartDestroyed.AddListener(HandlePartDestroyed);
        rb2D = GetComponent<Rigidbody2D>();
    }

    protected override void Update() {

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
        } else {
            //If we are not connected treat the object as a base obj
            base.Update();
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

    protected virtual void OnShipConnected() { }
    protected virtual void OnPartActivated() { }
    protected virtual void OnPartActivating() { }
    protected virtual void OnPartDeactivated() { }

    public void HandlePartDestroyed() {
        //Destroy object and invoke discconection from ship if the node no longer connects to base ship.
        foreach (Connection connection in connections) {
            if (connection.ConnectedPart) {
                connection.ConnectedPart.damagable.StartCoroutine(ChainedPartDestroy(connection.ConnectedPart.damagable));
            }
        }
        Destroy(gameObject);
    }

    private IEnumerator ChainedPartDestroy(Damagable damagable) {
        yield return new WaitForSeconds(chainDestroyDelay);
        damagable.TakeDamage(float.MaxValue);
    }
}
