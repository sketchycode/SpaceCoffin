using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Connectable {
    [SerializeField] private float thrustFactor = 5f;

    private bool isThrusting = false;

    protected override void OnPartActivated() {
        isThrusting = true;
    }

    protected override void OnPartDeactivated() {
        isThrusting = false;
    }

    private void LateUpdate() {
        if (isThrusting) {
            connectedShip.Rb2D.AddForceAtPosition(transform.up * thrustFactor, transform.position);
        }
    }
}
