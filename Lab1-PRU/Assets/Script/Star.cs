using UnityEngine;

public class Star : MonoBehaviour, ICollectable
{
    // fall speed of the star
    private float fallSpeed = 4f;

    //score for the star
    public int score = 1;
    // Implement the Collect method from the ICollectable interface
    public void Collect()
    {
        // Logic for collecting the star, e.g., increase score, play sound, etc.
        Debug.Log("Star collected!");
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.starSoundEffect, 1f); // Play star collect sound
        Destroy(gameObject); // Destroy the star object after collection
        GameController.instance.GainScore(score); // Call the GainScore method to increase the score
    }

    // check collision trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.transform.parent.CompareTag("Player"))
        {
            Collect();
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, -fallSpeed);
    }

}
