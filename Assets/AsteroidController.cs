using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject AsteroidPrefab;

    public float SpawnDelaySeconds = 10;

    private Camera _camera;
    private List<GameObject> asteroids;
    private float _timeSinceLastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeSinceLastSpawn >= SpawnDelaySeconds)
        {
            _timeSinceLastSpawn = 0;
            SpawnAsteroid();
        }
        else
            _timeSinceLastSpawn += Time.deltaTime;
    }

    private void SpawnAsteroid(float asteroidTier = 0)
    {
        Vector3 spawnPoint = new Vector3(0.25f, 0.25f, 0.0f);
        Vector3 spawnWorldPoint = _camera.WorldToScreenPoint(spawnPoint);

        GameObject asteroidGameObject = Instantiate(AsteroidPrefab, spawnPoint, new Quaternion(0,0,0,0));

        Asteroid asteroid = asteroidGameObject.GetComponent<Asteroid>();

        if (asteroidTier == 0)
            asteroidTier = Random.Range(1, 4);

        asteroid.AsteroidTier = asteroidTier;

        asteroid.DestroyEvent += Asteroid_DestroyEvent;
    }

    private void Asteroid_DestroyEvent(Asteroid asteroid)
    {
        if (asteroid.AsteroidTier != 1)
        {
            SpawnAsteroid(asteroid.AsteroidTier - 1f);
        }
    }
}
