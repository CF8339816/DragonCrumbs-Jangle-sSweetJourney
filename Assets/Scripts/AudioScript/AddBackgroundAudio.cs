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