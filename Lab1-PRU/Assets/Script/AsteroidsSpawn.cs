using System;
using System.Collections;
using UnityEngine;

public class AsteroidsSpawn : MonoBehaviour
{
    //number of asteroids
    private int numberOfAsteroids = 100;
    //asteroid prefab
    public GameObject asteroidPrefab;
    //asteroid spawn area
    public Vector3 spawnArea;

    // camera position
    private Vector3 cameraPosition;

    bool isSpawned = false;

    // current level of the game
    private int currentLevel = 1;


    //get area from camera
    void Start()
    {
        currentLevel = 1; // Initialize current level
        cameraPosition = Camera.main.transform.position;

        // Get camera dimensions
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;

        // Define spawn area: full camera width, just above the top of the camera view
        spawnArea = new Vector3(cameraWidth, cameraHeight, 0f);

        // Spawn asteroids above the camera view
        StartCoroutine(SpawnAsteroids(cameraPosition, cameraHeight, cameraWidth));
    }
    // Get the camera position and spawn asteroids above the camera view


    // Spawn asteroids above the camera view
    IEnumerator SpawnAsteroids(Vector3 cameraPosition, float cameraHeight, float cameraWidth)
    {
        isSpawned = true;
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            // Random X within camera width
            float spawnX = UnityEngine.Random.Range(cameraPosition.x - cameraWidth, cameraPosition.x + cameraWidth);
            // Y just above the camera view
            float spawnY = cameraPosition.y + cameraHeight + 1f;
            // Z as prefab's Z
            float spawnZ = cameraPosition.z + 2f; // Adjust Z position as needed

            // Add some randomization to Y
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

            // Get an asteroid from the pool
            GameObject asteroid = AsteroidsPool.Instance.GetObject();
            //place the asteroid at the spawn position
            if (asteroid != null)
            {
                asteroid.transform.position = spawnPosition;
                asteroid.transform.rotation = Quaternion.identity;
                asteroid.SetActive(true); // Activate the asteroid
            }
            else
            {
                // If no asteroid is available, log a warning and exit the coroutine
                Debug.LogWarning("No available asteroid in the pool. Consider increasing the pool size.");
            }
            if (asteroid == null)
            {
                Debug.LogWarning("No available asteroid in the pool. Consider increasing the pool size.");
                yield break; // Exit if no asteroid is available
            }
            //get a random speed for the asteroid
            float randomSpeed = UnityEngine.Random.Range(Math.Min(20f, 1f + currentLevel), Mathf.Min(25f, 6f + currentLevel));
            asteroid.GetComponent<Asteroids>().SetSpeed(randomSpeed); // Set the random speed to the asteroid
            float basetimeToLive = 12f; // Base time to live for the asteroid
            float timeToLive = basetimeToLive / asteroid.GetComponent<Asteroids>().GetSpeed(); // Adjust time to live based on asteroid speed
            
            // return the asteroid to the pool after timeToLive seconds
            StartCoroutine(ReturnAsteroidToPool(asteroid, timeToLive));

            // Delay before spawning the next asteroid
            float delay = UnityEngine.Random.Range(Math.Max(0.1f, 0.5f - currentLevel / 10f), Math.Max(0.5f,1f - currentLevel / 10f));
            yield return new WaitForSeconds(delay);
        }
        isSpawned = false; // Reset the spawn flag after spawning all asteroids
    }

    //check if asteroids are out of bounds
    void Update()
    {
        if (!isSpawned)
        {
            // Check if the camera is still active
            if (Camera.main != null)
            {
                // Get the camera position
                cameraPosition = Camera.main.transform.position;

                // Start spawning asteroids if not already spawned
                StartCoroutine(SpawnAsteroids(cameraPosition, Camera.main.orthographicSize, Camera.main.orthographicSize * Camera.main.aspect));
            }
        }
        // Check if the current level has changed
        if (GameController.instance.GetScore() / 10 >= currentLevel)
        {
            currentLevel++;
            UIController.instance.UpdateLevel(currentLevel);
            BackgroundScroller.Instance.SetScrollSpeed(BackgroundScroller.Instance.GetScrollSpeed() + 0.1f); // Increase background scroll speed
        }
    }
    // Return asteroid to the pool after timeToLive seconds
    private IEnumerator ReturnAsteroidToPool(GameObject asteroid, float timeToLive)
    {
        yield return new WaitForSeconds(timeToLive);
        AsteroidsPool.Instance.ReturnObject(asteroid);
    }
}
