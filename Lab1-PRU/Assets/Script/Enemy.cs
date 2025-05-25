using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float hitPoints;  // Dùng 'protected' để lớp con truy cập được

    public Enemy(float hp)
    {
        hitPoints = hp;
    }

    // get this hp
    public float GetHitPoints()
    {
        return hitPoints;
    }
    // set this hp
    public void SetHitPoints(float hp)
    {
        hitPoints = hp;
    }

    // Decrease hit points
    public virtual void DecreaseHitPoints(float amount)
    {
        hitPoints -= amount;
    }
}
