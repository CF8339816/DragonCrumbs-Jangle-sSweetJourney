using System.Collections;
using TMPro;
using UnityEngine;
using Unity.Cinemachine;
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

public class GameExitManager : MonoBehaviour
{
    public static GameExitManager Instance { get; private set; }
    public float ExitDelay = 5f;
    public TextMeshProUGUI exitCountdownText;
    private float Countdown;
    private bool isExiting = false;
    private AddAudio addAudio;
  

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessable accross scene changes

        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }

        FindCountdownText();
        ResetCountdown();
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
        FindCountdownText();
        if (!isExiting) ResetCountdown();
    }

    // [CODE REVIEW] Finds exitCountdownText even if it starts inactive in the UI hierarchy
    public void FindCountdownText()
    {
        if (exitCountdownText == null)
        {
            foreach (TextMeshProUGUI tmp in FindObjectsByType<TextMeshProUGUI>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (tmp.name == "exitCountdownText")
                {
                    exitCountdownText = tmp;
                    break;
                }
            }
        }
    }

    private void Update()
    {
        if (exitCountdownText == null) { FindCountdownText(); }
    }

    public void Ongameexit()
    {
        if (!isExiting)
        {
            isExiting = true;
            FindCountdownText();
            StartCoroutine(StartExitCountdown());
          
        }
    }

    public IEnumerator StartExitCountdown()
    {
        ResetCountdown();
        while (Countdown > .1f)
        {
            if (exitCountdownText != null)
            {
                exitCountdownText.text = "Game Exit in: " + Mathf.Ceil(Countdown).ToString();// displays the countdown output in an always rounded up to whole int
            }
            yield return null;
            Countdown -= Time.deltaTime;
        }
        if (exitCountdownText != null)
        {
            exitCountdownText.text = "Exiting...";
            addAudio.OnMiscDialogueGarble();
        }
        ExitGame();
    }

    private void ResetCountdown()
    {
        Countdown = ExitDelay;
        if (exitCountdownText != null)
        {
            exitCountdownText.text = "";
        }
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        // Works in a built application
        Application.Quit();
        // Works inside the Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}