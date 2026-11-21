//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//using UnityEngine.Rendering;
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

public class AddAudio : MonoBehaviour
{// Start is called once before the first execution of Update after the MonoBehaviour is created

    private AddBackgroundAudio backgroundMusicManager;

    [Header("audio clip")]
    [SerializeField] public AudioClip millFlour;// defines clip 
    [SerializeField] public AudioClip Success;// defines clip 
    [SerializeField] public AudioClip WingFlap;// defines clip 
    [SerializeField] public AudioClip FireBreath;// defines clip 
    [SerializeField] public AudioClip PickupSound;// defines clip 
    [SerializeField] public AudioClip WalkSound;// defines clip 

    [SerializeField] public AudioClip MiscDialogueGarble;// defines clip 
    [SerializeField] public AudioClip Landing;// defines clip 
    [SerializeField] public AudioClip VoicesNewSprites ;// defines clip 
    [SerializeField] public AudioClip VoiceDangle;// defines clip 
    [SerializeField] public AudioClip BeeHive;// defines clip 
    [SerializeField] public AudioClip SFX9;// defines clip 
    [SerializeField] public AudioClip SFX10;// defines clip 
    [SerializeField] public AudioClip SFX11;// defines clip 
    [SerializeField] public AudioSource sourceSFXandDialogue;// defines audio source
    private static AudioSource currentlyPlayingSource; //defines any currently playing for audio checks
    [SerializeField] private float startingVolume = .35f;


    [Header("UI Elements")]
    [SerializeField] public Slider volumeSlider; // defines  the slider ui element being used
    private float currentNormalizedVolume = 0.35f; // stores default volume across scenes

    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        //bootStrappin.FindManagers();

        //GameObject sliderObject = GameObject.Find("VolumeSFX");
        //if (sliderObject != null)
        //{
        //    volumeSlider = sliderObject.GetComponent<Slider>();
        //}

        if (sourceSFXandDialogue == null) sourceSFXandDialogue = GetComponent<AudioSource>();
        if (sourceSFXandDialogue == null) sourceSFXandDialogue = gameObject.AddComponent<AudioSource>();

        sourceSFXandDialogue.spatialBlend = 0f;
        sourceSFXandDialogue.playOnAwake = false;
        sourceSFXandDialogue.mute = false;

        currentNormalizedVolume = startingVolume;
        sourceSFXandDialogue.volume = currentNormalizedVolume;
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
        StartupAudio(); //finds the SFX slider in newly loaded scenes
    }

    private void Start()
    {
        StartupAudio();

    }
    public void StartupAudio()//slider name VolumeSFX
    {
        if (volumeSlider == null)
        {
            GameObject sliderObject = GameObject.Find("VolumeSFX");
            if (sliderObject != null)
            {
                volumeSlider = sliderObject.GetComponent<Slider>();
            }
        }

        sourceSFXandDialogue.volume = currentNormalizedVolume; // Safely sets 0.35f fallback (or saved volume)

        if (volumeSlider != null)
        {
            
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);// configures the slider 
            volumeSlider.minValue = 1f;                                 // for 1 to 100 whole-number 
            volumeSlider.maxValue = 100f;                               //increments without 
            volumeSlider.wholeNumbers = true;                           //snapping back

            volumeSlider.SetValueWithoutNotify(Mathf.Round(currentNormalizedVolume * 100f)); // provides 35 vol setting to UI without retriggering event
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    public void OnVolumeChanged(float newVolume)
    {
        currentNormalizedVolume = Mathf.Clamp01(newVolume / 100f);// converts 1-100 slider value into 0.01f-1.0f AudioSource volume
        if (sourceSFXandDialogue != null)  //allose for volumme to be changedc based on slider valuie during play 
        {
            sourceSFXandDialogue.volume = newVolume;
        }
    }
    private void ApplyCurrentVolume()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)
        {
            currentNormalizedVolume = Mathf.Clamp01(volumeSlider.value / 100f);
            sourceSFXandDialogue.volume = currentNormalizedVolume;
        }
        else if (sourceSFXandDialogue != null)
        {
            sourceSFXandDialogue.volume = currentNormalizedVolume;
        }
    }
    public void OnMillFlour()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && millFlour != null)
        {
            sourceSFXandDialogue.PlayOneShot(millFlour, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }


    public void OnSuccess()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && Success != null)
        {
            sourceSFXandDialogue.PlayOneShot(Success, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnWingFlap()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && WingFlap != null)
        {
            sourceSFXandDialogue.clip = WingFlap;//plays defined audio clip on ente of collider zone
            sourceSFXandDialogue.loop = true;
            sourceSFXandDialogue.Play();
        }
    }
    public void StopWingFlap()
    {
        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip == WingFlap)
        {
            sourceSFXandDialogue.Stop();
            sourceSFXandDialogue.loop = false;
        }
    }
    public void OnFireBreath()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && FireBreath != null)
        {
            sourceSFXandDialogue.clip = FireBreath; //plays defined audio clip on ente of collider zone
            sourceSFXandDialogue.loop = true;
            sourceSFXandDialogue.Play();


        }
    }
    public void StopFireBreath()
    {
        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip == FireBreath)
        {
            sourceSFXandDialogue.Stop();
            sourceSFXandDialogue.loop = false;
        }
    }
    public void OnPickupSound()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && PickupSound != null)
        {
            sourceSFXandDialogue.PlayOneShot(PickupSound, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnWalkSound()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && WalkSound != null)
        {
            sourceSFXandDialogue.clip = WalkSound;//plays defined audio clip on ente of collider zone
            sourceSFXandDialogue.loop = true;
            sourceSFXandDialogue.Play();
        }
    }

    public void StopWalk()
    {
        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip == WalkSound)
        {
            sourceSFXandDialogue.Stop();
            sourceSFXandDialogue.loop = false;
        }
    }

    public void OnMiscDialogueGarble()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && MiscDialogueGarble != null)
        {
            sourceSFXandDialogue.PlayOneShot(MiscDialogueGarble, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnLanding()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && Landing != null)
        {
            sourceSFXandDialogue.PlayOneShot(Landing, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnVoicesNewSprites()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && VoicesNewSprites != null) // [CODE REVIEW] Respect volume slider instead of forcing 1f
        {
            sourceSFXandDialogue.clip = VoicesNewSprites;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }

    public void StopVoicesNewSprites()
    {
        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip == VoicesNewSprites)
        {
            sourceSFXandDialogue.Stop();
            sourceSFXandDialogue.loop = false;
        }
    }

    public void OnVoiceDangle()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && VoiceDangle != null)
        {
            sourceSFXandDialogue.clip = VoiceDangle;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }

    public void StopVoiceDangle()
    {
        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip == VoiceDangle)
        {
            sourceSFXandDialogue.Stop();
            sourceSFXandDialogue.loop = false;
        }
    }

    public void OnBeeHive()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && BeeHive != null)
        {
            sourceSFXandDialogue.clip = BeeHive;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    
    public void StopBeeHivek()
    {
        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip == BeeHive)
        {
            sourceSFXandDialogue.Stop();
            sourceSFXandDialogue.loop = false;
        }
    }
    public void OnSFX9()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && SFX9 != null)
        {
            sourceSFXandDialogue.clip = SFX9;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnSFX10()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && SFX10 != null)
        {
            sourceSFXandDialogue.clip = SFX10;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnSFX11()
    {
        ApplyCurrentVolume();
        if (sourceSFXandDialogue != null && SFX11 != null)
        {
            sourceSFXandDialogue.clip = SFX11;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
}