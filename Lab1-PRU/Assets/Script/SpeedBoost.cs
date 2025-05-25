using System.Collections;
using UnityEngine;

public class SpeedBoost : MonoBehaviour, IBoost, ICollectable
{
    // modifier for the speed boost
    private float speedModifier = 2f;
    // duration of the speed boost
    private float duration = 15f;

    // speed of boost
    private float speed = 4f;

    public void Collect()
    {
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.collectBuffSoundEffect, 1f); // Play speed boost collect sound
    }

    public void EnterBoost(GameObject player)
    {
        player.GetComponentInChildren<PlayerMovement>().speedModifier = speedModifier;
        Collect(); // Call the Collect method to play sound
    }

    public void ExitBoost(GameObject player)
    {
        player.GetComponentInChildren<PlayerMovement>().speedModifier = 1f; // Reset speed modifier to default
        Destroy(gameObject); // Destroy the speed boost object after use
    }

    public float GetDuration()
    {
        return duration;
    }
    void Update()
    {
        // Move the speed boost downwards
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -speed);
    }
}
