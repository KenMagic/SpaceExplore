using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    //Singleton instance of AudioManager
    public static AudioManager Instance { get; private set; }
    // Audio source for background music
    public AudioSource backgroundMusicSource;
    // Audio source for sound effects
    public AudioSource soundEffectsSource;
    // Audio clip for background music
    public AudioClip backgroundMusicClip;
    // Audio clip for ingame bgm
    public AudioClip ingameBGMClip;
    // Shoot sound effect
    public AudioClip shootSoundEffect;
    // Explosion sound effect
    public AudioClip explosionSoundEffect;

    // collect buff sound effect
    public AudioClip collectBuffSoundEffect;

    //star sound effect
    public AudioClip starSoundEffect;

    //rare star sound effect
    public AudioClip rareStarSoundEffect;
    //lose sound effect
    public AudioClip loseSoundEffect;
    //click sound effect
    public AudioClip clickSoundEffect;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensure only one instance of AudioManager exists
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
        ChangeBGM(backgroundMusicClip); // Set the initial background music
    }

    // Update is called once per frame
    void Update()
    {
        // mouse click sound effect
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            PlaySoundEffect(clickSoundEffect,1f); // Play click sound effect on mouse click
        }
    }
    // Method to change bgm
    public void ChangeBGM(AudioClip newClip)
    {
        if (backgroundMusicSource != null && newClip != null)
        {
            backgroundMusicSource.clip = newClip; // Set the new background music clip
            backgroundMusicSource.Play(); // Play the new background music
                                          //check volume
            Debug.Log("Background music changed to: " + newClip.name + " with volume: " + backgroundMusicSource.volume);
        }
    }
    // Method to play any sound effect
    public void PlaySoundEffect(AudioClip clip, float volume = 1f)
    {
        if (soundEffectsSource != null && clip != null)
        {
            Debug.Log("Playing sound effect: " + clip.name + " with volume: " + volume);
            soundEffectsSource.PlayOneShot(clip, volume); // Play the sound effect
        }
    }
}
