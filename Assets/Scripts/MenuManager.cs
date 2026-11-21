using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
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

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private GameExitManager gameExitManager;
    public ObjectButtonsInteractables objectButtonsInteractables;

    [SerializeField] GameObject Jangle;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject StartSplash;

    private const int STARTHUB_INDEX = 0;
    private const int QUEST1A_INDEX = 1;
    private const int QUEST1B_INDEX = 2;
    private const int QUEST1C_INDEX = 3;
    private const int BAKERY_INDEX = 4;
    private const int TRAVELMAP_INDEX = 5;

    public int STARTHUB;
    public int QUEST1A;
    public int QUEST1B;
    public int QUEST1C;
    public int BAKERYX;
    public int TRAVELMAP;

    public bool hasActiveQuest = false;
    private int RETURNLEVEL_INDEX;
    public int ReturnIndex;
    private bool pendingQuestState = false;
    private bool pendingBakeryState = false;
    public int nextIndex;
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

        if (Instructions != null) Instructions.SetActive(false);
        Debug.Log("CustomSceneManager awakening...");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessible across scene changes

        //AutoHookupMenuButtons();


        STARTHUB = STARTHUB_INDEX;
        QUEST1A = QUEST1A_INDEX;
        QUEST1B = QUEST1B_INDEX;
        QUEST1C = QUEST1C_INDEX;
        BAKERYX = BAKERY_INDEX;
        TRAVELMAP = TRAVELMAP_INDEX;

    }
    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void LoadLevelByIndex(int index)
    {
        if (index >= 0 && index <= 5)
        {
            SceneManager.LoadScene(index);

        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.buildIndex == STARTHUB_INDEX)
        {
            if (Instructions == null) Instructions = GameObject.Find("Instructions");
            if (StartSplash == null) StartSplash = GameObject.Find("StartSplash");
        }

        if (pendingQuestState)
        {
            pendingQuestState = false;

            //finds the script in the newly loaded quest scene
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

            if (objectButtonsInteractables != null)
            {
                Debug.Log("Setting QuestState");
                objectButtonsInteractables.SetQuestState();
                Debug.Log("QuestState set");
            }
            else
            {
                Debug.LogWarning("ObjectButtonsInteractables was not found in " + scene.name);
            }
        }
        if (pendingBakeryState)
        {
            pendingBakeryState = false;

            // finds the script in the newly loaded quest scene
            objectButtonsInteractables = FindFirstObjectByType<ObjectButtonsInteractables>();

            if (objectButtonsInteractables != null)
            {
                Debug.Log("Setting BakeryState");
                objectButtonsInteractables.SetBakeryState();
                Debug.Log("BakeryState set");
            }
            else
            {
                Debug.LogWarning("ObjectButtonsInteractables was not found in " + scene.name);
            }
        }

    }

    public void onLevel1()
    {
        pendingQuestState = true;
        pendingBakeryState = false;
        if (Jangle != null)
        {
            Jangle.SetActive(true);
        }
        LoadLevelByIndex(QUEST1A_INDEX);
        //Debug.Log("Setting QuestState");
        //objectButtonsInteractables.SetQuestState();
        //Debug.Log("QuestState set");
    }

    public void onLevel2()
    {
        pendingQuestState = true;
        pendingBakeryState = false;
        if (Jangle != null)
        {
            Jangle.SetActive(true);
        }
        LoadLevelByIndex(QUEST1B_INDEX);
        //Debug.Log("Setting QuestState");
        //objectButtonsInteractables.SetQuestState();
        //Debug.Log("QuestState set");
    }

    public void onLevel3()
    {
        pendingQuestState = true;
        pendingBakeryState = false;
        if (Jangle != null)
        {
            Jangle.SetActive(true);
        }
        LoadLevelByIndex(QUEST1C_INDEX);
        //Debug.Log("Setting QuestState");
        //objectButtonsInteractables.SetQuestState();
        //Debug.Log("QuestState set");
    }

    public void onBakery()
    {
        if (hasActiveQuest)
        {
            pendingQuestState = true;
            pendingBakeryState = false;
        }
        else
        {
            pendingQuestState = false;
            pendingBakeryState = true;
        }
        LoadLevelByIndex(BAKERY_INDEX);
    }


    public void onTravel()
    {

        if (Jangle != null)
        {
            Jangle.SetActive(false);
        }
        ReturnIndex = SceneManager.GetActiveScene().buildIndex;
        LoadLevelByIndex(TRAVELMAP_INDEX);
    }



    public void onBegin()
    {
        if (StartSplash != null) StartSplash.SetActive(false);
        if (Instructions != null) Instructions.SetActive(true);

    }
    public void onReselect()
    {
        if (StartSplash != null) StartSplash.SetActive(true);
        if (Instructions != null) Instructions.SetActive(false);

    }
    public void onStart()
    {
        hasActiveQuest = false;
        if (StartSplash != null) StartSplash.SetActive(false);
        if (Instructions != null) Instructions.SetActive(false);
        onBakery();
    }


    public void onLoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIndex >= 1 && nextIndex <= 3)
            {
                hasActiveQuest = true;
                pendingQuestState = true;
                pendingBakeryState = false;
            }
            else if (nextIndex == 4)
            {
                pendingQuestState = hasActiveQuest;
                pendingBakeryState = !hasActiveQuest;
                LoadLevelByIndex(nextIndex);


            }
            else
            {
                onMenu();

            }
        }
    }

    public void onMenu()
    {
        hasActiveQuest = false; // [CODE REVIEW] Reset active quest flag when returning to main hub
        LoadLevelByIndex(STARTHUB_INDEX);
        // SceneManager.LoadScene(STARTHUB_INDEX, LoadSceneMode.Additive);

    }




}
