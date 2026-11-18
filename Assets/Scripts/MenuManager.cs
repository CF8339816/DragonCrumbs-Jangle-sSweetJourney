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
    private ObjectButtonsInteractables objectButtonsInteractables;

    [SerializeField] GameObject Jangle;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject StartSplash;

    private const int STARTHUB_INDEX = 0;
    private const int QUEST1A_INDEX = 1;
    private const int QUEST1B_INDEX = 2;
    private const int QUEST1C_INDEX = 3;
    private const int BAKERY_INDEX = 4;
   
    private const int TRAVELMAP_INDEX = 5;
 
    private int RETURNLEVEL_INDEX;
    public int ReturnIndex;
 

    private int nextIndex;
    public void Awake()
    {
        Instructions.SetActive(false);
        Debug.Log("CustomSceneManager awakening...");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessible across scene changes

        //AutoHookupMenuButtons();

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
        //AutoHookupMenuButtons();
    }
    //private void AutoHookupMenuButtons()
    //{
    //    Button startButton = GameObject.Find("Start")?.GetComponent<Button>();
    //    if (startButton != null)
    //    {
    //        startButton.onClick.RemoveAllListeners();
    //        startButton.onClick.AddListener(onStart);
    //    }
    //    Button BeginButton = GameObject.Find("Begin")?.GetComponent<Button>();
    //    if (BeginButton != null)
    //    {
    //        BeginButton.onClick.RemoveAllListeners();
    //        BeginButton.onClick.AddListener(onStart);
    //    }
    //    Button ReselectButton = GameObject.Find("Reselect")?.GetComponent<Button>();
    //    if (ReselectButton != null)
    //    {
    //        ReselectButton.onClick.RemoveAllListeners();
    //        ReselectButton.onClick.AddListener(onReselect);
    //    }

    //    Button RepickButton = GameObject.Find("RepickButton")?.GetComponent<Button>();
    //    if (RepickButton != null)
    //    {
    //        RepickButton.onClick.RemoveAllListeners();
    //        RepickButton.onClick.AddListener(objectButtonsInteractables.OnRecipeRePick);
    //    }
    //    Button AcceptButton = GameObject.Find("AcceptButton")?.GetComponent<Button>();
    //    if (AcceptButton != null)
    //    {
    //        AcceptButton.onClick.RemoveAllListeners();
    //        AcceptButton.onClick.AddListener(objectButtonsInteractables.OnApplePieStart);
    //    }
    //    Button OkButton = GameObject.Find("OkButton")?.GetComponent<Button>();
    //    if (OkButton != null)
    //    {
    //        OkButton.onClick.RemoveAllListeners();
    //        OkButton.onClick.AddListener(objectButtonsInteractables.OnRecipeOK);
    //    }
     

    //    Button QuitButton = GameObject.Find("Quit")?.GetComponent<Button>();
    //    if (QuitButton != null)
    //    {
    //        QuitButton.onClick.RemoveAllListeners();


    //        if (GameExitManager.Instance != null)
    //        {
    //            QuitButton.onClick.AddListener(GameExitManager.Instance.Ongameexit);
    //        }
    //    }

    //}
    public void LoadLevelByIndex(int index)
    {
        if (index >= 0 && index <7) 
        {
            SceneManager.LoadScene(index);
    
        }
    }
    public void onLevel1()
    {
        LoadLevelByIndex(QUEST1A_INDEX);
        Debug.Log("Setting QuestState");
        objectButtonsInteractables.SetQuestState();
        Debug.Log("QuestState set");
    }

    public void onLevel2()
    {
        LoadLevelByIndex(QUEST1B_INDEX);
        Debug.Log("Setting QuestState");
        objectButtonsInteractables.SetQuestState();
        Debug.Log("QuestState set");
    }

    public void onLevel3()
    {
        LoadLevelByIndex(QUEST1C_INDEX);
        Debug.Log("Setting QuestState");
        objectButtonsInteractables.SetQuestState();
        Debug.Log("QuestState set");
    }

    public void onBakery()
    {
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
        StartSplash.SetActive(false);
        Instructions.SetActive(true);

    }
    public void onReselect()
    {
        StartSplash.SetActive(true);
        Instructions.SetActive(false);

    }
    public void onStart()
    {
        StartSplash.SetActive(false);
        Instructions.SetActive(false);
        onBakery();
        }


    public void onLoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < 4)
        {
            nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            LoadLevelByIndex(nextIndex);


        }
        else
        {
            onMenu();

        }
    }

         public void onMenu()
    {
        LoadLevelByIndex(STARTHUB_INDEX);
        SceneManager.LoadScene(STARTHUB_INDEX, LoadSceneMode.Additive);
      
    }




}
   
  

