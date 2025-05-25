using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    //Singleton instance
    public static GameController instance;

    //score variable
    private int score;

    //Awake is called when the script instance is being loaded
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
            // Make sure this object persists across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If instance already exists, destroy this object
            Destroy(gameObject);
        }
    }

    // Pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
    }
    // Resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Set time scale to 1 to resume the game
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0; // Initialize score to 0
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Toggle pause/resume when Escape key is pressed
            if (Time.timeScale == 1f)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    //Gain score method
    public void GainScore(int points)
    {
        score += points; // Increase score by the specified points
        UIController.instance.UpdateScore(score); // Update the score display in the UI
    }

    // Get the current score
    public int GetScore()
    {
        return score; // Return the current score
    }

    // End the game
    public void EndGame()
    {
        Debug.Log("Game Over! Final Score: " + score); // Log the final score
        PauseGame(); // Pause the game
        AudioManager.Instance.backgroundMusicSource.Stop(); // Stop the background music
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.loseSoundEffect); // Play the lose sound effect
        UIController.instance.ShowNewGameButton(); // Show the New Game button in the UI        
    }
}
