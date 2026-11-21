//using Unity.VisualScripting;
using UnityEngine;
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

    private BackgroundMusicManager backgroundMusicManager;

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

        GameObject sliderObject = GameObject.Find("Volume");
        if (sliderObject != null)
        {
            volumeSlider = sliderObject.GetComponent<Slider>();
        }

        if (sourceSFXandDialogue == null) sourceSFXandDialogue = GetComponent<AudioSource>();
        if (sourceSFXandDialogue == null) sourceSFXandDialogue = gameObject.AddComponent<AudioSource>();

        sourceSFXandDialogue.spatialBlend = 0f;
        sourceSFXandDialogue.playOnAwake = false;
        sourceSFXandDialogue.mute = false;

    }
    private void Start()
    {
        StartupAudio();

    }
    public void StartupAudio()
    {
        sourceSFXandDialogue.volume = startingVolume; // Safely sets 0.35f fallback

        if (volumeSlider != null)
        {
            volumeSlider.minValue = 0f;
            volumeSlider.maxValue = 1f;


            if (volumeSlider.value != 1f && volumeSlider.value != 0f)// If another script already adjusted the slider from 1.0, match it
            {
                sourceSFXandDialogue.volume = volumeSlider.value;
            }
            else
            {
                volumeSlider.value = sourceSFXandDialogue.volume; // Handshake 0.35f to UI
            }

            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        }
    }

    public void OnVolumeChanged(float newVolume)
    {
        if (sourceSFXandDialogue != null)  //allose for volumme to be changedc based on slider valuie during play 
        {
            sourceSFXandDialogue.volume = newVolume;
        }
    }

    public void OnMillFlour()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && millFlour != null)
        {
            sourceSFXandDialogue.PlayOneShot(millFlour, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }


    public void OnSuccess()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume
                = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && Success != null)
        {
            sourceSFXandDialogue.PlayOneShot(Success, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnWingFlap()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && WingFlap != null)
        {
            sourceSFXandDialogue.PlayOneShot(WingFlap, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnFireBreath()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && FireBreath != null)
        {
            sourceSFXandDialogue.PlayOneShot(FireBreath, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnPickupSound()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && PickupSound != null)
        {
            sourceSFXandDialogue.PlayOneShot(PickupSound, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnWalkSound()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && WalkSound != null)
        {
            sourceSFXandDialogue.PlayOneShot(WalkSound, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnMiscDialogueGarble()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && MiscDialogueGarble != null)
        {
            sourceSFXandDialogue.PlayOneShot(MiscDialogueGarble, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnLanding()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && Landing != null)
        {
            sourceSFXandDialogue.PlayOneShot(Landing, 1.0f);//plays defined audio clip on ente of collider zone
        }
    }
    public void OnVoicesNewSprites()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && VoicesNewSprites != null) // [CODE REVIEW] Respect volume slider instead of forcing 1f
        {
            sourceSFXandDialogue.clip = VoicesNewSprites;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnVoiceDangle()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && VoiceDangle != null)
        {
            sourceSFXandDialogue.clip = VoiceDangle;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnBeeHive()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && BeeHive != null)
        {
            sourceSFXandDialogue.clip = BeeHive;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnSFX9()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && SFX9 != null)
        {
            sourceSFXandDialogue.clip = SFX9;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnSFX10()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && SFX10 != null)
        {
            sourceSFXandDialogue.clip = SFX10;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
    public void OnSFX11()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }
        if (sourceSFXandDialogue != null && SFX11 != null)
        {
            sourceSFXandDialogue.clip = SFX11;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
        }
    }
}