using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Bullet speed
    public float speed = 10f;

    // Rigidbody2D component
    public Rigidbody2D rb;

    // bullet damage
    public int damage = 1;

    // multiplier for damage
    public float damageModifier = 1f;

    // Destroy time
    public float destroyTime = 1f;

    //Update is called once per frame
    void Update()
    {
        Fire(Vector2.up);
    }

    // Bullet flight
    public void Fire(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction.normalized * speed;
        Destroy(gameObject, destroyTime);
    }

    public float GetDamage()
    {
        return damage*damageModifier;
    }
}
