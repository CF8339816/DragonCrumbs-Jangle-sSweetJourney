using UnityEngine;
using UnityEngine.SceneManagement;
#region coder & project
/// <summary>
/// NSCC GAME2065/4087/Game Development III(B)/Cameron,Jordan
/// Jam 3 :DragonCrumbs: Jangle's Sweet Journey
/// team: Chris French, Percy Hebb, Kevin Card, Myranda Roy
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// code review: 
/// </summary>
#endregion

public class BackgroundMusicManager : MonoBehaviour
{


    private AddAudio addAudio;
    public AudioSource musicSource; // Dedicated purely to the background music
    [SerializeField] public AudioClip backgroundTrackBakery;
    [SerializeField] public AudioClip backgroundTrackDefault;

    private const int BAKERY_INDEX = 4;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayAlwaysOnTrack();
    }

    void Start()
    {
        PlayAlwaysOnTrack();
    }

    public void PlayAlwaysOnTrack()
    {
        if (musicSource != null && backgroundTrackBakery != null && backgroundTrackDefault != null)
        {
           
            AudioClip targetTrack = (SceneManager.GetActiveScene().buildIndex == BAKERY_INDEX)// plays the bakery audio 
                ? backgroundTrackBakery
                : backgroundTrackDefault;

            musicSource.loop = true; // Ensures it never stops
            musicSource.playOnAwake = true; // Optional: plays instantly when scene loads

           
            if (musicSource.clip != targetTrack || !musicSource.isPlaying)// ensures an audio hand off for the scenes
            {
                musicSource.clip = targetTrack;
                musicSource.Play();
            }
        }
    }
}