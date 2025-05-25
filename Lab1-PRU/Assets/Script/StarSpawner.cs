using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    // Singleton instance
    public static StarSpawner Instance { get; private set; }
    public Vector3 spawnArea; // area in which stars will be spawned
    private Vector3 cameraPosition; // position of the camera
    //list of many kinds of stars
    private List<StarType> starTypes; // list of star prefabs

    // cooldown time for spawning stars
    private float spawnCooldown = 2f; // Adjust as needed
    bool isSpawned = false; // flag to check if stars are spawned

    void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        cameraPosition = Camera.main.transform.position;
        spawnArea = new Vector3(cameraPosition.x, cameraPosition.y, 0f);
        // Initialize the star types list
        starTypes = new List<StarType>();
        AddStarType(new StarType
        {
            starPrefab = Resources.Load<GameObject>("Prefabs/Star"), // Load your star prefab
            rarity = 1f // Set the rarity for this star type
        });
        AddStarType(new StarType
        {
            starPrefab = Resources.Load<GameObject>("Prefabs/SpeedBoost"), // Load your rare star prefab
            rarity = 0.1f // Set the rarity for this star type
        });
        AddStarType(new StarType
        {
            starPrefab = Resources.Load<GameObject>("Prefabs/SuperStar"), // Load your super star prefab
            rarity = 0.005f // Set the rarity for this star type
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawned && Random.Range(0f, 1f) < 0.01f) // Combine both conditions
        {
            SpawnStars();
        }
    }

    // Method to spawn stars
    void SpawnStars()
    {
        isSpawned = true; // Set the flag to true to prevent multiple spawns
        // Get camera dimensions
        Camera camera = Camera.main;
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        // Random X within camera width
        float spawnX = Random.Range(cameraPosition.x - cameraWidth, cameraPosition.x + cameraWidth);
        // Y just above the camera view
        float spawnY = cameraPosition.y + cameraHeight + 1f;
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        StarType starType = GetRandomStarType();
        if (starType != null && starType.starPrefab != null)
        {
            GameObject star = Instantiate(starType.starPrefab, spawnPosition, Quaternion.identity);
            // Destroy the star after a certain time
            Destroy(star, 5f); // Adjust the time as needed
        }
        //Delay the next spawn
        StartCoroutine(SpawnCooldown());
    }

    // Method to get a random star type
    public StarType GetRandomStarType()
    {
        if (starTypes == null || starTypes.Count == 0)
        {
            Debug.LogWarning("Star types list is empty or not initialized.");
            return null;
        }

        float totalRarity = 0f;
        foreach (var starType in starTypes)
        {
            totalRarity += starType.rarity;
        }

        float randomValue = Random.Range(0f, totalRarity);
        float cumulativeRarity = 0f;

        foreach (var starType in starTypes)
        {
            cumulativeRarity += starType.rarity;
            if (randomValue <= cumulativeRarity)
            {
                return starType;
            }
        }

        return null; // Fallback in case no type is selected
    }

    // Method to add a star type to the list
    public void AddStarType(StarType starType)
    {
        if (starTypes == null)
        {
            starTypes = new List<StarType>();
        }
        starTypes.Add(starType);
    }

    // Coroutine to handle spawn cooldown
    private IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(spawnCooldown);
        isSpawned = false; // Reset the flag after cooldown
    }

    // Spawn star in a specific position
    public void SpawnStar(Vector3 position)
    {
        StarType starType = GetRandomStarType();
        if (starType != null && starType.starPrefab != null)
        {
            GameObject star = Instantiate(starType.starPrefab, position, Quaternion.identity);
            // destroy the star after a certain time
            Destroy(star, 5f); // Adjust the time as needed
        }
    }
}
