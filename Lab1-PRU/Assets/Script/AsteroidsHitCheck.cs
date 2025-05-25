using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidsHitCheck : MonoBehaviour
{
    // On collision with the player trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        // Check if the collided object has the tag "Player"
        if (collision.CompareTag("Bullet"))
        {
            Hit(collision.GetComponentInParent<Bullet>());
            // Destroy the bullet
            Destroy(collision.transform.parent.gameObject);
            Debug.Log("Bullet destroyed");

        }
        else if (collision.transform.parent.CompareTag("Player"))
        {
            //return the asteroid to the pool
            AsteroidsPool.Instance.ReturnObject(gameObject);
            //play death anim
            gameObject.GetComponent<Asteroids>().DeathAnimation();
            Debug.Log("Player hit by asteroid");
            GameController.instance.EndGame();
            Debug.Log("Asteroid destroyed by player");
        }
        else
        {
            Debug.Log("Collision with non-player object: " + collision.gameObject.name);
        }
    }

    // calculate hit
    private void Hit(Bullet bullet)
    {
        // get this hp
        Enemy enemy = GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.GetHitPoints();
            // Decrease hit points
            enemy.DecreaseHitPoints(bullet.GetDamage());
            Debug.Log("Hit points after hit: " + enemy.GetHitPoints());

        }
    }

}
