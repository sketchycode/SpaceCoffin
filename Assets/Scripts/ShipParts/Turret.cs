using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Connectable {
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform barrel;

    protected override void OnPartActivated() {
        var bullet = Instantiate(bulletPrefab, barrel.position, barrel.rotation);
    }
}
