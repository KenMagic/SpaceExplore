using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Player movement speed
    public float speed = 3f;

    // Modifier for movement speed
    public float speedModifier = 1f;

    // Reference to the Rigidbody2D component
    public Rigidbody2D rb;

    // Bullet prefab
    public GameObject bulletPrefab;

    // Shooting cooldown
    public float shootCooldown = 0.5f;

    // Where to shoot
    public Transform shootPoint;

    //Is player shooting
    private bool isShooting = false;

    // Attack speed modifier
    public float attackSpeedModifier = 1f;

    //Update is called once per frame
    private void Start()
    {
        isShooting = false;
    }
    void Update()
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is not assigned.");
        }
        Movement();
        Shoot();
        Limit();
    }
    //Movement
    void Movement()
    {
        Vector2 velocity = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            velocity.y += speed * speedModifier;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            velocity.y -= speed * speedModifier;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            velocity.x -= speed * speedModifier;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            velocity.x += speed * speedModifier;
        }

        rb.linearVelocity = velocity;
    }

    void Shoot()
    {
        if (isShooting)
            return;
        isShooting = true;
        // Instantiate a bullet at the player's position
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.shootSoundEffect, 0.3f); // Play shooting sound
        StartCoroutine(ShootCooldown());
    }
    IEnumerator ShootCooldown()
    {
        // Wait for the cooldown period
        yield return new WaitForSeconds(shootCooldown / attackSpeedModifier);
        isShooting = false;
    }
    
    //limit player movement to screen bounds
    private void Limit()
    {
        Camera camera = Camera.main;
        if (camera != null)
        {
            Vector3 min = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
            Vector3 max = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, min.x + 0.5f, max.x - 0.5f);
            position.y = Mathf.Clamp(position.y, min.y + 0.5f, max.y - 0.5f);
            transform.position = position;

            // Prevent velocity from moving player outside bounds
            Vector2 clampedVelocity = rb.linearVelocity;
            if ((position.x <= min.x + 0.5f && clampedVelocity.x < 0) || (position.x >= max.x - 0.5f && clampedVelocity.x > 0))
                clampedVelocity.x = 0;
            if ((position.y <= min.y + 0.5f && clampedVelocity.y < 0) || (position.y >= max.y - 0.5f && clampedVelocity.y > 0))
                clampedVelocity.y = 0;
            rb.linearVelocity = clampedVelocity;
        }
    }
}
