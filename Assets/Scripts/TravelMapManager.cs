using UnityEngine;
using TMPro;
using System.Collections;
public class TravelMapManager : MonoBehaviour
{
    public static TravelMapManager Instance { get; private set; }
    public float StageLoadDelay = 5f;
    //public TextMeshProUGUI exitCountdownText;
    private float Countdown;
    private bool isLoading = false;
   [SerializeField] private ObjectButtonsInteractables objectButtonsInteractables;
    [SerializeField] private MenuManager menuManager;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessable accross scene changes
        ResetCountdown();
    }
    //private void Update()
    //{
    //    if (exitCountdownText == null) { exitCountdownText = GameObject.Find("exitCountdownText")?.GetComponent<TextMeshProUGUI>(); }
    //}
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
        while (Countdown > 0)
        {
            //exitCountdownText.text = "Game Exit in: " + Mathf.Ceil(Countdown).ToString();// displays the countdown output in an always rounded up to whole int
            yield return null;
            Countdown -= Time.deltaTime;
        }
        isLoading = false;
        ResetCountdown();
        //exitCountdownText.text = "Exiting...";
        LoadQuestLevel();
    }
    private void ResetCountdown()
    {
        Countdown = StageLoadDelay;
        //if (exitCountdownText != null)
        //{
        //    exitCountdownText.text = "";
        //}
    }
    public void LoadQuestLevel()
    {
        if (objectButtonsInteractables.currentQuest == 0)
        {
            menuManager.onLevel1();
        }




    }
}