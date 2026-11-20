using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        //Changed cause the code below throws a couple of random errors depending on load orders ensures first load 
        //BootStrappin bootStrappin = FindFirstObjectByType<BootStrappin>();
        //bootStrappin.FindManagers();

        //if (Instance != null && Instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        //Instance = this;
        //DontDestroyOnLoad(gameObject); // keeps script accessable accross scene changes
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
            // [CODE REVIEW] Default to quest 0 (Apple Pie) if ObjectButtonsInteractables was unloaded with the Bakery scene
            int questIndex = (objectButtonsInteractables != null) ? objectButtonsInteractables.currentQuest : 0;

            if (questIndex == 0)
            {
                // [CODE REVIEW] Null-safe check in case StageInteractManager isn't in the TravelMap scene
                if (stageInteractManager == null || (stageInteractManager.AppleCount < 1 && stageInteractManager.FlourCount < 1 && stageInteractManager.HoneyCount < 1))
                {
                    menuManager.onLevel1();
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
                    menuManager.onLoadNextLevel();
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
                    menuManager.onBakery();
                }
            }
        }
        else
        {
            Debug.LogWarning("TravelMapManager: Missing MenuManager!");
        }
    }
}