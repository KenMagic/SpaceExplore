using UnityEngine;

public class UIStartControl : MonoBehaviour
{
    // Function to start the game
    public void StartGame()
    {
        LoadScreenManager.Instance.LoadSceneWithLoadingScreen("SampleScene");
        AudioManager.Instance.ChangeBGM(AudioManager.Instance.ingameBGMClip);
    }
}
