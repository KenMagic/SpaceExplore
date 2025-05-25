using UnityEngine;

public interface IBoost
{
    //Enter the boost
    void EnterBoost(GameObject player);
    //Exit the boost
    void ExitBoost(GameObject player);
    //Get the boost duration
    float GetDuration();
}
