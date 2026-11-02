//using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Rendering;
using UnityEngine.UI;

#region coder & project
/// <summary>
/// NSCC GAME2065/4087/Game Development III(B)/Cameron,Jordan
/// Jam 2 :Monster Mixer
/// team: Chris French, Benjamin Mackey, Arianna Cutler
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
    [SerializeField] public AudioClip ClimbLadder;// defines clip 
    [SerializeField] public AudioClip SFX1;// defines clip 
    [SerializeField] public AudioClip SFX2;// defines clip 
    [SerializeField] public AudioClip SFX3;

    [SerializeField] public AudioClip SFX4;// defines clip 
    [SerializeField] public AudioClip SFX5;// defines clip 
    [SerializeField] public AudioClip SFX6;// defines clip 
    [SerializeField] public AudioClip SFX7;// defines clip 
    [SerializeField] public AudioClip SFX8;// defines clip 
    [SerializeField] public AudioClip SFX9;// defines clip 
    [SerializeField] public AudioClip SFX10;// defines clip 
    [SerializeField] public AudioClip SFX11;// defines clip 
    [SerializeField] public AudioSource sourceSFXandDialogue;// defines audio source
    private static AudioSource currentlyPlayingSource; //defines any currently playing for audio checks
    [SerializeField] private float startingVolume = .35f;


    [Header("UI Elements")]
    [SerializeField] public Slider volumeSlider; // defines  the slider ui element being used

    private void Awake()
    {
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

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(millFlour, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }


    public void OnSuccess()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(Success, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }


    public void OnClimbLadder()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(ClimbLadder, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }

    public void OnSFX1()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(SFX1, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }
    public void OnSFX2()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(SFX2, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }
    public void OnSFX3()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(SFX3, 1.0f);//plays defined audio clip on ente of collider zone

        }
    }
    public void OnSFX4()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(SFX4, 1.0f);//plays defined audio clip on ente of collider zone


        }

    }
    public void OnSFX5()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.PlayOneShot(SFX5, 1.0f);//plays defined audio clip on ente of collider zone


        }

    }
    public void OnSFX6()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.volume = 2f;
            sourceSFXandDialogue.clip = SFX6;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();

        }

    }
    public void OnSFX7()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.volume = 2f;
            sourceSFXandDialogue.clip = SFX7;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();

        }

    }
    public void OnSFX8()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.volume = 2f;
            sourceSFXandDialogue.clip = SFX8;
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

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.volume = 2f;
            sourceSFXandDialogue.clip = SFX9;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();
            ;
        }


    }
    public void OnSFX10()
    {
        if (volumeSlider != null && sourceSFXandDialogue != null)// presets volume per new govenrences above
        {
            sourceSFXandDialogue.volume = volumeSlider.value;
        }

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.volume = 2f;
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

        if (sourceSFXandDialogue != null && sourceSFXandDialogue.clip != null)
        {
            sourceSFXandDialogue.volume = 2f;
            sourceSFXandDialogue.clip = SFX11;
            sourceSFXandDialogue.loop = false;
            sourceSFXandDialogue.Play();

        }

    }


}
