using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
#region coder & project
/// <summary>
/// NSCC GAME2025 / 4086 / Game Programming III(B)/ Doucette,Matthew
/// Engine Programming Review 05  UI Management (UIManager)
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// 
/// </summary>
#endregion
public class Custom_sceneManager : MonoBehaviour
{

    public static Custom_sceneManager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private MenuManager menuManager;
    private HudManager hudManager;
    //private PauseManager pauseManager;

    [SerializeField] GameObject Jangle;
    [SerializeField] GameObject Instructions;

    private const int STARTHUB_INDEX = 0;
    private const int QUEST1A_INDEX = 1;
    private const int QUEST1B_INDEX = 2;
    private const int QUEST1C_INDEX = 3;
    private const int BAKERY_INDEX = 4;
    private const int QUESTHUD_INDEX = 5;
    private const int TRAVELMAP_INDEX = 6;
    private const int PAUSE_INDEX = 7;
    // private const int PLAYER_INDEX = 7;
    // private const int ENVIRONMENT_INDEX = 8;
    //private const int SHOP_INDEX = 9;
    private int RETURNLEVEL_INDEX;
    private int ReturnIndex;

    void Awake()
    {
        Debug.Log("CustomSceneManager awakening...");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessible across scene changes

        //Debug.Log(" Menu  awakening...");
        //SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
        //Debug.Log(" menu loaded...");


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
        //MenuManager.AutoHookupMenuButtons();
        //AutoHookupHUDButtons();
        //AutoHookupPauseButtons();
        //AutoHookupShopButtons();
        //AutoHookupHudOutputs();

    }
}