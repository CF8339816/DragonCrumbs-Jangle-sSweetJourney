using UnityEngine;
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
    [SerializeField] public AudioClip backgroundTrack;
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
    }
    void Start()
    {
        PlayAlwaysOnTrack();
    }

    public void PlayAlwaysOnTrack()
    {
        if (musicSource != null && backgroundTrack != null)
        {
            musicSource.clip = backgroundTrack;
            musicSource.loop = true; // Ensures it never stops
            musicSource.playOnAwake = true; // Optional: plays instantly when scene loads

            if (!musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }
}