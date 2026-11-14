using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
#region coder & project
/// <summary>
/// NSCC GAME2025 / 4086 / Game Programming III(B)/ Doucette,Matthew
/// Unity: Game Manager & Persistence
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// 
/// </summary>
#endregion
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;

    //[SerializeField] GameObject Jangle;
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject StartSplash;

    private const int STARTHUB_INDEX = 0;
    private const int QUEST1A_INDEX = 1;
    private const int QUEST1B_INDEX = 2;
    private const int QUEST1C_INDEX = 3;
    private const int BAKERY_INDEX = 4;
    private const int QUESTHUD_INDEX = 5;
    private const int TRAVELMAP_INDEX = 6;
    private const int PAUSE_INDEX = 7;
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
    private void AutoHookupMenuButtons()
    {
        //Button startButton = GameObject.Find("StartGame")?.GetComponent<Button>();
        //if (startButton != null)
        //{
        //    startButton.onClick.RemoveAllListeners();
        //    startButton.onClick.AddListener(onStart);
        //}
        //Button nextButton = GameObject.Find("nextLevel")?.GetComponent<Button>();
        //if (nextButton != null)
        //{
        //    nextButton.onClick.RemoveAllListeners();
        //    nextButton.onClick.AddListener(onLoadNextLevel);
        //}
        //Button menuButton = GameObject.Find("Menu")?.GetComponent<Button>();
        //if (menuButton != null)
        //{
        //    menuButton.onClick.RemoveAllListeners();
        //    menuButton.onClick.AddListener(onMenu);
        //}
        //Button level1Button = GameObject.Find("Level1")?.GetComponent<Button>();
        //if (level1Button != null)
        //{
        //    level1Button.onClick.RemoveAllListeners();
        //    level1Button.onClick.AddListener(onLevel1);
        //}
        //Button level2Button = GameObject.Find("level2")?.GetComponent<Button>();
        //if (level2Button != null)
        //{
        //    level2Button.onClick.RemoveAllListeners();
        //    level2Button.onClick.AddListener(onLevel2);
        //}
        //Button level3Button = GameObject.Find("level3")?.GetComponent<Button>();
        //if (level3Button != null)
        //{
        //    level3Button.onClick.RemoveAllListeners();
        //    level3Button.onClick.AddListener(onLevel3);
        //}
        //Button QuitButton = GameObject.Find("Quit")?.GetComponent<Button>();

        //if (QuitButton != null)
        //{
        //    QuitButton.onClick.RemoveAllListeners();


        //    if (GameExitManager.Instance != null)
        //    {
        //        QuitButton.onClick.AddListener(GameExitManager.Instance.Ongameexit);
        //    }
        //}

    }
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
             
    }

    public void onLevel2()
    {
        LoadLevelByIndex(QUEST1B_INDEX);

    }

    public void onLevel3()
    {
        LoadLevelByIndex(QUEST1C_INDEX);

    }

    public void onBakery()
    {
        LoadLevelByIndex(BAKERY_INDEX);

    }


    public void onPause()
    {


        if (gameManager.Jangle != null)
        {
            gameManager.Jangle.SetActive(false);
        }
        ReturnIndex = SceneManager.GetActiveScene().buildIndex;
        LoadLevelByIndex(PAUSE_INDEX);
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
}
    //public void onLoadNextLevel()
    //{
    //    if (SceneManager.GetActiveScene().buildIndex < 3)
    //    { 
    //        nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
    //    LoadLevelByIndex(nextIndex);
    //    SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
    //    SceneManager.LoadScene(HUD_INDEX, LoadSceneMode.Additive);
    //    SceneManager.LoadScene(PLAYER_INDEX, LoadSceneMode.Additive);
    //    SceneManager.LoadScene(ENVIRONMENT_INDEX, LoadSceneMode.Additive);
    //     }
    //    else
    //    {
    //        onMenu();
    //    }
        
    //}

  

