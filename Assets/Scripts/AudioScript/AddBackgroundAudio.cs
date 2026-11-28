using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

public class AddBackgroundAudio : MonoBehaviour
{
    private AddAudio addAudio;
    public AudioSource musicSource; // Dedicated purely to the background music
    [SerializeField] public AudioClip backgroundTrackBakery;
    [SerializeField] public AudioClip backgroundTrackDefault;

    [Header("Music Volume Control (1-100)")]
    [SerializeField] public Slider VolumeBG; //second slider for background music
    [SerializeField] private float startingMusicVolume = 0.25f; //lower default so SFX aren't drowned out
    private float currentMusicVolume = 0.25f;

    private const int BAKERY_INDEX = 4;

    private void Awake()
    {
        currentMusicVolume = startingMusicVolume;
        if (musicSource == null) musicSource = GetComponent<AudioSource>();
        if (musicSource != null) musicSource.volume = currentMusicVolume;
    }

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
        SetupMusicSlider();
        PlayAlwaysOnTrack();
    }

    void Start()
    {
        SetupMusicSlider();
        PlayAlwaysOnTrack();
    }

    // [CODE REVIEW] Connects a second UI slider named "MusicVolume" (1 to 100 increments)
    public void SetupMusicSlider()
    {
        if (VolumeBG == null)
        {
            GameObject sliderObj = GameObject.Find("MusicVolume");
            if (sliderObj != null)
            {
                VolumeBG = sliderObj.GetComponent<Slider>();
            }
        }

        if (musicSource != null)
        {
            musicSource.volume = currentMusicVolume;
        }

        if (VolumeBG != null)
        {
            VolumeBG.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            VolumeBG.minValue = 1f;
            VolumeBG.maxValue = 100f;
            VolumeBG.wholeNumbers = true;

            VolumeBG.SetValueWithoutNotify(Mathf.Round(currentMusicVolume * 100f));
            VolumeBG.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
    }

    public void OnMusicVolumeChanged(float newVolume)
    {
        currentMusicVolume = Mathf.Clamp01(newVolume / 100f);
        if (musicSource != null)
        {
            musicSource.volume = currentMusicVolume;
        }
    }

    public void PlayAlwaysOnTrack()
    {
        if (musicSource != null && backgroundTrackBakery != null && backgroundTrackDefault != null)
        {

            AudioClip targetTrack = (SceneManager.GetActiveScene().buildIndex == BAKERY_INDEX)// plays the bakery audio 
                ? backgroundTrackBakery
                : backgroundTrackDefault;

            musicSource.volume = currentMusicVolume;
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