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
   [SerializeField] private ObjectButtonsInteractables objectButtonsInteractables;
    [SerializeField] private MenuManager menuManager;
    public string travelMapSceneName = "TravelMap";

    void Awake()
    {
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
        else
        {
            Debug.LogWarning("TravelMapManager: Missing MenuManager or ObjectButtonsInteractables!");
        }



    }
}