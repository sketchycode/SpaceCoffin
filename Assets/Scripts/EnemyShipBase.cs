using UnityEngine;

public class EnemyShipBase : MonoBehaviour
{
    public float TurnRate = 1   ;

    private Transform _playerShip;

    // Start is called before the first frame update
    void Start()
    {
        //Grab player ship upon instantiation
        _playerShip = FindObjectOfType<ShipCore>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Point towards player ship
        //Move towards player ship
        // If collide explode and remove node.
        FindPlayer();
    }

    private void FindPlayer()
    {
        if (_playerShip != null)
            transform.up = Vector3.Lerp(transform.up, (_playerShip.position - transform.position), TurnRate);
        //transform.LookAt(_playerShip, Vector3.back);
    }
}
