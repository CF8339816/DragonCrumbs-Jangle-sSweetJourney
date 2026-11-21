using System.Collections;
using TMPro;
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
public class TravelMapManager : MonoBehaviour
{
    public static TravelMapManager Instance { get; private set; }
    public float StageLoadDelay = 5f;
    //public TextMeshProUGUI exitCountdownText;
    private float Countdown;
    private bool isLoading = false;
    private ObjectButtonsInteractables objectButtonsInteractables;
    private MenuManager menuManager;
    private StageInteractManager stageInteractManager;
    public string travelMapSceneName = "TravelMap";

    void Awake()
    {
        BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        if (bootStrappin != null)
        {
            bootStrappin.FindManagers();
        }
       
        ResetCountdown();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == travelMapSceneName)
        {
            ResetCountdown();
            onStageLoad();
        }
    }

    public void onStageLoad()
    {
        if (!isLoading)
        {
            isLoading = true;

            StartCoroutine(StartLoadCountdown());


        }
    }
    public IEnumerator StartLoadCountdown()
    {
        while (Countdown > .25f)
        {

            yield return null;
            Countdown -= Time.deltaTime;
        }
        isLoading = false;
        ResetCountdown();

        LoadQuestLevel();

    }
    private void ResetCountdown()
    {
        Countdown = StageLoadDelay;

    }
    private void FindLocalReferences()
    {
        if (BootStrappin.Instance != null)
        {
            BootStrappin.Instance.FindManagers();
            if (menuManager == null) menuManager = BootStrappin.Instance.menuManager;
            if (objectButtonsInteractables == null) objectButtonsInteractables = BootStrappin.Instance.objectButtonsInteractables;
            if (stageInteractManager == null) stageInteractManager = BootStrappin.Instance.stageInteractManager;
        }

        if (menuManager == null) menuManager = FindFirstObjectByType<MenuManager>();
        if (objectButtonsInteractables == null) objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();
        if (stageInteractManager == null) stageInteractManager = FindFirstObjectByType<StageInteractManager>();
    }

    public void LoadQuestLevel()
    {
        FindLocalReferences();

        if (menuManager != null)
        {
            
            int questIndex = (objectButtonsInteractables != null) ? objectButtonsInteractables.currentQuest : 0;// default to quest 0 (Apple Pie) if ObjectButtonsInteractables was unloaded with the Bakery scene

            if (questIndex == 0)
            {
                
                if (stageInteractManager == null || (stageInteractManager.AppleCount < 1 && stageInteractManager.FlourCount < 1 && stageInteractManager.HoneyCount < 1))// null-safe check in case StageInteractManager isn't in the TravelMap scene
                {
                    stageInteractManager.InteractNextLevelDebug();
                    // menuManager.onLevel1();//commented out to allow for the testing button to drive the to next level for the moment
                }
            }
        }
        else
        {
            Debug.LogWarning("TravelMapManager: Missing MenuManager!");
        }
    }

    public void NextQuestLevel()
    {
        FindLocalReferences();

        if (menuManager != null)
        {
            int questIndex = (objectButtonsInteractables != null) ? objectButtonsInteractables.currentQuest : 0;

            if (questIndex == 0)
            {
                if (stageInteractManager == null || stageInteractManager.AppleCount < 5 || stageInteractManager.FlourCount < 1 || stageInteractManager.HoneyCount < 1)
                {
                    stageInteractManager.InteractNextLevelDebug();
                    // menuManager.onLoadNextLevel();//commented out to allow for the testing button to drive the to next level for the moment
                }
            }
        }
        else
        {
            Debug.LogWarning("TravelMapManager: Missing MenuManager!");
        }
    }

    public void LastQuestLevel()
    {
        FindLocalReferences();

        if (menuManager != null)
        {
            int questIndex = (objectButtonsInteractables != null) ? objectButtonsInteractables.currentQuest : 0;

            if (questIndex == 0)
            {
                if (stageInteractManager != null && stageInteractManager.AppleCount == 5 && stageInteractManager.FlourCount == 1 && stageInteractManager.HoneyCount == 1)
                {
                    stageInteractManager.InteractNextLevelDebug();
                    //menuManager.onBakery();//commented out to allow for the testing button to drive the to next level for the moment
                }
            }
        }
        else
        {
            Debug.LogWarning("TravelMapManager: Missing MenuManager!");
        }
    }
}