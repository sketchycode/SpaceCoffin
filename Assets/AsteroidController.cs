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

    private void SpawnAsteroid(float asteroidTier = 0, Vector3? position = null)
    {
        GameObject asteroidGameObject;

        //handle position of asteroid
        if (position != null)
            asteroidGameObject = Instantiate(AsteroidPrefab, (Vector3)position, new Quaternion(0, 0, 0, 0));
        else
        {
            float randomPosX = Random.Range(0.0f, 1.0f) * _camera.pixelWidth;
            float randomPosY = Random.Range(0.0f, 1.0f) * _camera.pixelHeight;
            position = new Vector3(randomPosX, randomPosY, 0.0f);

            RaycastHit hit;

            
            Vector3 spawnWorldPoint = _camera.ScreenToWorldPoint((Vector3)position);

            spawnWorldPoint.z = 0;

            if (!Physics.SphereCast(spawnWorldPoint, 1.0f, transform.forward, out hit))
            {
                asteroidGameObject = Instantiate(AsteroidPrefab, spawnWorldPoint, new Quaternion(0, 0, 0, 0));
            }
            else
            {
                Debug.Log($"Asteroid would collide with {hit.collider.name}");
                return;
            }

            
        }

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
            SpawnAsteroid(asteroid.AsteroidTier - 1f, asteroid.gameObject.transform.position);
            SpawnAsteroid(asteroid.AsteroidTier - 1f, asteroid.gameObject.transform.position);
        }
    }
}
