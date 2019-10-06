using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject AsteroidPrefab;

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnAsteroid();
    }

    private void SpawnAsteroid()
    {
        Vector3 spawnPoint = new Vector3(0.25f, 0.25f, 0.0f);
        Vector3 spawnWorldPoint = _camera.WorldToScreenPoint(spawnPoint);

        //Instantiate(AsteroidPrefab, spawnPoint, new Quaternion(0,0,0,0));
    }
}
