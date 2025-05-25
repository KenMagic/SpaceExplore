using Unity.VisualScripting;
using UnityEngine;

public class Asteroids : Enemy
{
    // Asteroid speed
    private float speed = 7f;

    public Asteroids() : base(1)
    {
    }
    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -speed);
    }

    // Get speed
    public float GetSpeed()
    {
        return speed;
    }
    //Set speed
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    // Override the DecreaseHitPoints method to handle asteroid-specific behavior
    public override void DecreaseHitPoints(float amount)
    {
        base.DecreaseHitPoints(amount);
        // Check if hit points are less than or equal to zero
        if (GetHitPoints() <= 0)
        {
            // Return the asteroid to the pool
            AsteroidsPool.Instance.ReturnObject(gameObject);
            // Spawn a star with rare chance
            if (Random.Range(0f, 1f) < 0.1f) // 10% chance to spawn a star
            {
                StarSpawner.Instance.SpawnStar(transform.position);
                Debug.Log("Star spawned from asteroid");
            }
            // spawn explosion effect
            DeathAnimation();
        }
        // Destroy the explosion effect after 2 seconds
    }
    // Death animation
    public void DeathAnimation()
    {
        GameObject explosion = Instantiate(Resources.Load("Prefabs/SplashDestroy"), transform.position, Quaternion.identity) as GameObject;
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.explosionSoundEffect, 1f); // Play asteroid destroy sound
        Debug.Log("Asteroid destroyed");
        Destroy(explosion, 0.2f);
    }

    //reset the asteroid
    public void ResetAsteroid()
    {
        // Reset hit points
        SetHitPoints(1);
        // Reset speed
        SetSpeed(7f);
    }

}
