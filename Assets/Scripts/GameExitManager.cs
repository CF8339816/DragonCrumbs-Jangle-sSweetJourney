using System.Collections;
using TMPro;
using UnityEngine;
using Unity.Cinemachine;
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
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 


        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        // bootStrappin.FindManagers();

        ResetCountdown();
    }

    private void Update()
    {
        if (exitCountdownText == null) { exitCountdownText = GameObject.Find("exitCountdownText")?.GetComponent<TextMeshProUGUI>(); }
    }
    public void Ongameexit()
    {
        if (!isExiting)
        {
            isExiting = true;
            StartCoroutine(StartExitCountdown());
            ExitGame();
        }
    }
    public IEnumerator StartExitCountdown()
    {
        while (Countdown > .1f)
        {
            exitCountdownText.text = "Game Exit in: " + Mathf.Ceil(Countdown).ToString();// displays the countdown output in an always rounded up to whole int
            yield return null;
            Countdown -= Time.deltaTime;
        }
        exitCountdownText.text = "Exiting...";

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