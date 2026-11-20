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
    public void LoadQuestLevel()
    {
        if (menuManager == null)
            menuManager = FindFirstObjectByType<MenuManager>();

        if (objectButtonsInteractables == null)
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

        if (menuManager != null && objectButtonsInteractables != null)
        {
            if (objectButtonsInteractables.currentQuest == 0)
            {
                menuManager.onLevel1();
            }
        }

        else if (menuManager != null && objectButtonsInteractables != null)
        {
            if (objectButtonsInteractables.currentQuest == 0)
            {
                if (stageInteractManager.AppleCount < 5 || stageInteractManager.FlourCount < 1 || stageInteractManager.HoneyCount < 1)
                menuManager.onLoadNextLevel();
            }
        }
        
        else if (menuManager != null && objectButtonsInteractables != null)
        {
            if (objectButtonsInteractables.currentQuest == 0)
            {
                if (stageInteractManager.AppleCount == 5 && stageInteractManager.FlourCount == 1 && stageInteractManager.HoneyCount == 1)
                    menuManager.onBakery();
            }
        }
        else
        {
                       
            Debug.LogWarning("TravelMapManager: Missing MenuManager or ObjectButtonsInteractables!");
        }



    }
}