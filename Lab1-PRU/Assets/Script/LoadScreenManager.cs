using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreenManager : MonoBehaviour
{
    // Singleton
    public static LoadScreenManager Instance { get; private set; }
    //GameObject to hold the loading screen UI
    public GameObject loadingScreenUI;
    // Slider to show loading progress
    public Slider loadingProgressSlider;

    void Awake()
    {
        // Ensure only one instance of LoadScreenManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //switch to a scene with a loading screen
    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        // Check if loadingScreenUI is assigned
        if (loadingScreenUI != null)
        {
            loadingScreenUI.SetActive(true);
        }

        // Check if loadingProgressSlider is assigned
        if (loadingProgressSlider != null)
        {
            loadingProgressSlider.value = 0f; // Reset the slider value
        }
        StartCoroutine(LoadingProgressAsync(sceneName)); // Start the loading coroutine
    }
    // Method to update the loading progress slider
    IEnumerator LoadingProgressAsync(String scene)
    {
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        while (!asyncOperation.isDone)
        {
            loadingProgressSlider.value = asyncOperation.progress;
            yield return null; // Wait for the next frame
        }
        yield return new WaitForSeconds(0.1f); // Ensure the last frame is processed
        if (loadingScreenUI != null)
        {
            loadingScreenUI.SetActive(false); // Hide the loading screen UI after loading
        }
    }
}
