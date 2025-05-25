using UnityEngine;

public class ApplyBoost : MonoBehaviour
{
    // plaer
    public GameObject player;

    // boost
    IBoost boost;


    // start
    private void Start()
    {
        // get the player
        player = GameObject.FindGameObjectWithTag("Player");
        boost = GetComponent<IBoost>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the player collided with the speed boost
        if (collision.transform.parent.CompareTag("Player"))
        {
            player.GetComponentInChildren<PlayerBoosts>().ApplyBoost(boost);
            // Destroy the boost
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
