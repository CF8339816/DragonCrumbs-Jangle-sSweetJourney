using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
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

public static class PerformBootstrap
{
    const string SceneName = "_BootStrap_";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void ExecuteBootstrap66()
    {
        for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)// checks through loaded scenes for the bootstrap scene
        {
            var candidate = SceneManager.GetSceneAt(sceneIndex);

            if (candidate.name == SceneName)// checks if scene already loaded an stops process
            {
                return;
            }

            Debug.Log("Loading Bootstrp scene" + SceneName);
        }

        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);  // loads bootstrap adatively

    }
}

public class BootStrappin : MonoBehaviour
{
    [Header("System References")]// defines other scripts

   
    public MenuManager menuManager;
    public DialogManager dialogManager;
    public GameExitManager gameExitManager;
    public InteractableManager interactableManager;
    public ItemManager itemManager;
    public ObjGOInteractionManager objGOInteractionManager;

    public GameObject mainCharacterCameraPrefab;
    public GameObject cinemachineMoveCamPrefab;

    public GameObject eventSystemPrefab;

    private const int STARTHUB_INDEX = 0;
    private const int QUEST1A_INDEX = 1;
    private const int QUEST1B_INDEX = 2;
    private const int QUEST1C_INDEX = 3;
    private const int BAKERY_INDEX = 4;
    private const int QUESTHUD_INDEX = 5;
    private const int TRAVELMAP_INDEX = 6;
    private const int PAUSE_INDEX = 7;
  

    public Camera ActiveMainCamera { get; private set; }
    public CinemachineCamera ActiveMoveCam { get; private set; } 
    public static BootStrappin Instance { get; private set; } = null;
    void Awake()
    {


        if (Instance != null && Instance != this)// checks for multiples
        {
            Debug.Log("Bootstrap found on " + gameObject.name);

            Destroy(gameObject); //destroy whole object
            return;
        }

        Debug.Log("Bootstrap Initalized!!!");
        Instance = this;

        DontDestroyOnLoad(gameObject); // prevents  boostrapper from being destroyed on multiscene load
        Debug.Log("Initalize Event System.");

        InitializeGlobalEventSystem();
        Debug.Log("EventSystem Initalized!!!!!");
        Debug.Log("Initalizing camera system");
        InitializeGlobalCameraSystem();
        Debug.Log("Cameras initalized Initalized!!!");

        //Debug.Log(" Menu  awakening...");
        //SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
        //Debug.Log(" menu loaded...");
     
        if (gameExitManager == null) gameExitManager = GetComponent<GameExitManager>();
      if (dialogManager == null) dialogManager = GetComponent< DialogManager> ();
        if (menuManager == null) menuManager = GetComponent<MenuManager>();
        if (interactableManager == null) interactableManager = GetComponent<InteractableManager>();
        if (itemManager == null) itemManager = GetComponent<ItemManager>();
        if (objGOInteractionManager == null) objGOInteractionManager = GetComponent<ObjGOInteractionManager>();

    }

    public void InitializeGlobalEventSystem()
    {
        EventSystem existingSystem = Object.FindFirstObjectByType<EventSystem>();// checks for extra event systems

        if (existingSystem != null)
        {
            DontDestroyOnLoad(existingSystem.gameObject); // keeps it from being destroyed.
        }
        else if (eventSystemPrefab != null)
        {
            GameObject spawnedSystem = Instantiate(eventSystemPrefab);// creates one if  not found
            spawnedSystem.name = "Global_EventSystem";
            DontDestroyOnLoad(spawnedSystem);
        }
    }
    public void InitializeGlobalCameraSystem()
    {
    
        GameObject existingMainCam = GameObject.Find("MainCharacterCamera");// sets up main character cam
        if (existingMainCam != null)
        {
            ActiveMainCamera = existingMainCam.GetComponent<Camera>();
            DontDestroyOnLoad(existingMainCam);
        }
        else if (mainCharacterCameraPrefab != null)
        {
            GameObject spawnedMain = Instantiate(mainCharacterCameraPrefab);
            spawnedMain.name = "MainCharacterCamera";
            ActiveMainCamera = spawnedMain.GetComponent<Camera>();
            DontDestroyOnLoad(spawnedMain);
        }

       
        GameObject existingCineCam = GameObject.Find("CinemachineCameraMainMoveCam");// sets up Cinemachine brain system
        if (existingCineCam != null)
        {
            ActiveMoveCam = existingCineCam.GetComponent<CinemachineCamera>();
            DontDestroyOnLoad(existingCineCam);
        }
        else if (cinemachineMoveCamPrefab != null)
        {
            GameObject spawnedCine = Instantiate(cinemachineMoveCamPrefab);
            spawnedCine.name = "CinemachineCameraMainMoveCam";
            ActiveMoveCam = spawnedCine.GetComponent<CinemachineCamera>();
            DontDestroyOnLoad(spawnedCine);
        }

        //Camera existingCamera = Object.FindFirstObjectByType<Camera>();
        //if (existingCamera != null)
        //{
        //    DontDestroyOnLoad(existingCamera.gameObject);
        //}
        //else if (cameraPrefab != null)
        //{
        //    GameObject spawnedCamera = Instantiate(MainCharacterCamera);
        //    spawnedCamera.name = "Global_CameraSystem";
        //    DontDestroyOnLoad(spawnedCamera);
        //}
    }

    public void TestBS()
    {
        Debug.Log("Bootstrap Functioning correctly" + gameObject.name);

    }

}
//// Start is called once before the first execution of Update after the MonoBehaviour is created
//void Start()
//{

//}
