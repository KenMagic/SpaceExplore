using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Instance of the UIController
    public static UIController instance;

    // Text field for displaying the score
    public TMPro.TextMeshProUGUI scoreText;

    // Text for level
    public TMPro.TextMeshProUGUI levelText;

    // New Game button
    public Button newGameButton;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        else
        {
            // If instance already exists, destroy this object
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the score text
        if (scoreText != null)
        {
            scoreText.text = "Score: 0"; // Set initial score to 0
        }
        else
        {
            Debug.LogWarning("Score text field is not assigned in the UIController.");
        }
        newGameButton.gameObject.SetActive(false); // Hide the New Game button initially
        if (levelText != null)
        {
            levelText.text = "Level: 1"; // Set initial level to 1
        }
        else
        {
            Debug.LogWarning("Level text field is not assigned in the UIController.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method to update the score text
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
        else
        {
            Debug.LogWarning("Score text field is not assigned in the UIController.");
        }
    }

    // Method to show the New Game button
    public void ShowNewGameButton()
    {
        if (newGameButton != null)
        {
            newGameButton.gameObject.SetActive(true); // Show the New Game button
        }
        else
        {
            Debug.LogWarning("New Game button is not assigned in the UIController.");
        }
    }

    // Method to handle the New Game button click
    public void OnNewGameButtonClick()
    {
        // Logic to start a new game, e.g., reset score, load the game scene, etc.
        GameController.instance.GainScore(-GameController.instance.GetScore()); // Reset score
        GameController.instance.ResumeGame(); // End the current game
        SceneManager.LoadScene("Start"); // Load the game scene
        AudioManager.Instance.ChangeBGM(AudioManager.Instance.backgroundMusicClip); // Play background music
    }

    // Method to update the level text
    public void UpdateLevel(int level)
    {
        if (levelText != null)
        {
            levelText.text = "Level: " + level.ToString();
        }
        else
        {
            Debug.LogWarning("Level text field is not assigned in the UIController.");
        }
    }

}
