using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour {
    private ConnectPoint[] connectPoints;

    public ShipCore Ship { get; set; }
    public Connectable ConnectedPart { get; set; }

    private void Awake() {
        connectPoints = GetComponentsInChildren<ConnectPoint>();
        foreach (var connectPt in connectPoints) {
            connectPt.OnConnected += HandleConnectPointConnected;
        }
    }

    private void HandleConnectPointConnected(ConnectPoint connectPt, Connectable connectable) {
        connectable.transform.SetParent(transform, true);
        Ship.RegisterConnectable(connectable);
        ConnectedPart = connectable;
    }
}
