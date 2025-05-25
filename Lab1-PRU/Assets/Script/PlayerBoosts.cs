using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoosts : MonoBehaviour
{
    //Dictionary to hold the boosts
    private Dictionary<string, IBoost> boosts = new Dictionary<string, IBoost>();
    //List to hold the active boosts
    private List<IBoost> activeBoosts = new List<IBoost>();
    // Start is called before the first frame update
    public GameObject player;


    // Handle the boost
    public void ApplyBoost(IBoost boost)
    {
        //Check if the boost is already active
        if (!activeBoosts.Contains(boost))
        {
            //Add the boost to the active boosts
            activeBoosts.Add(boost);
            //Enter the boost
            boost.EnterBoost(player);
            //Start the coroutine to exit the boost after the duration
            StartCoroutine(ExitBoostAfterDuration(boost));
        }
    }
    //Exit the boost after the duration
    private IEnumerator ExitBoostAfterDuration(IBoost boost)
    {
        //Wait for the duration of the boost
        yield return new WaitForSeconds(boost.GetDuration());
        //Exit the boost
        boost.ExitBoost(player);
        //Remove the boost from the active boosts
        activeBoosts.Remove(boost);
    }
    // add a boost to the dictionary
    public void AddBoost(string key, IBoost boost)
    {
        if (!boosts.ContainsKey(key))
        {
            boosts.Add(key, boost);
        }
    }
    // get a boost from the dictionary
    public IBoost GetBoost(string key)
    {
        if (boosts.ContainsKey(key))
        {
            return boosts[key];
        }
        return null;
    }
    // if the player has a boost renew it
    public void AddBoostTime(string key)
    {
        if (boosts.ContainsKey(key))
        {
            IBoost boost = boosts[key];
            // Check if the boost is active
            if (activeBoosts.Contains(boost))
            {
                // Exit the boost first
                boost.ExitBoost(player);
                // Start the coroutine to re-enter the boost with additional time
                StartCoroutine(ExitBoostAfterDuration(boost));
            }
            // Re-enter the boost with the new duration
            boost.EnterBoost(player);
        }
    }
    
}
